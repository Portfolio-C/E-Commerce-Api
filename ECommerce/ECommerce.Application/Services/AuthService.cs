using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Auth;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services;

internal sealed class AuthService(
    IApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    ITokenHandler tokenHandler) : IAuthService
{

    public async Task<TokenDto> LoginAsync(LoginRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await context.ApplicationsUsers
            .FirstOrDefaultAsync(au => au.UserName == request.Username);

        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new InvalidLoginAttemptException("Invalid email or password");
        }

        var roles = await userManager.GetRolesAsync(user);
        var accessToken = tokenHandler.GenerateAccessToken(user, roles);
        var refreshToken = tokenHandler.GenerateRefreshToken();
        var tokenEntity = CreateTokenEntity(user, refreshToken);

        context.RefreshTokens.Add(tokenEntity);
        await context.SaveChangesAsync();

        return new TokenDto(accessToken, refreshToken);
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        await ValidateUserAsync(request);

        var user = CreateUserEntity(request);
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new RegistrationFailedException($"User registration failed: {errors}");
        }
    }

    public async Task<TokenDto> RefreshTokenAsync(RefreshTokenRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var token = await GetAndValidateRefreshTokenAsync(request.RefreshToken);
        var user = await GetAndValidateUserAsync(token.UserId);
        var roles = await userManager.GetRolesAsync(user);
        var accessToken = tokenHandler.GenerateAccessToken(user, roles);
        var refreshToken = tokenHandler.GenerateRefreshToken();

        await ReplaceRefreshTokenAsync(user, request.RefreshToken, refreshToken);

        return new TokenDto(accessToken, refreshToken);
    }

    private async Task ValidateUserAsync(RegisterRequest request)
    {
        if (await userManager.FindByNameAsync(request.Username) is not null)
        {
            throw new UserAlreadyExistsException($"User with username `{request.Username}` already exists.");
        }

        if (await userManager.FindByEmailAsync(request.Email) is not null)
        {
            throw new UserAlreadyExistsException($"User with email `{request.Username}` already exists.");
        }
    }

    private async Task<RefreshToken> GetAndValidateRefreshTokenAsync(string refreshToken)
    {
        var token = await context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (token is null || token.ExpiresAtUtc <DateTime.UtcNow || token.IsRevoked)
        {
            throw new SecurityTokenException("Refresh token is invalid.");
        }

        return token;
    }

    private async Task<ApplicationUser> GetAndValidateUserAsync(string userId)
    {
        var user = await context.ApplicationsUsers
            .FirstOrDefaultAsync(au => au.Id == userId);

        return user is null ? throw new EntityNotFoundException($"User with id: {userId} is not found.") : user;
    }

    private async Task ReplaceRefreshTokenAsync(ApplicationUser user, string oldRefreshToken, string newRefreshToken)
    {
        await RevokeOldTokenAsync(oldRefreshToken);

        var tokenEntity = CreateTokenEntity(user, newRefreshToken);
        
        context.RefreshTokens.Add(tokenEntity);
        await context.SaveChangesAsync();
    }

    private async Task RevokeOldTokenAsync(string oldRefreshToken)
    {
        var existingToken = await context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == oldRefreshToken);

        if (existingToken is not null)
        {
            existingToken.IsRevoked = true;
            await context.SaveChangesAsync();
        }
    }

    private static ApplicationUser CreateUserEntity(RegisterRequest request) =>
        new()
        {
            UserName = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = request.Role
        };

    private static RefreshToken CreateTokenEntity(ApplicationUser user, string token) =>
        new()
        {
            Token = token,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            UserId = user.Id,
            User = user,
        };
}

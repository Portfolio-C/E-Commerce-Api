using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.Requests;

namespace ECommerce.Application.Interfaces;

public interface IAuthService
{
    Task<TokenDto> LoginAsync(LoginRequest request);
    Task RegisterAsync(RegisterRequest request);
    Task<TokenDto> RefreshTokenAsync(RefreshTokenRequest request);
}

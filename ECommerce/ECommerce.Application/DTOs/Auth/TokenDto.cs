namespace ECommerce.Application.DTOs.Auth;

public sealed record TokenDto(string AccessToken, string RefreshToken);

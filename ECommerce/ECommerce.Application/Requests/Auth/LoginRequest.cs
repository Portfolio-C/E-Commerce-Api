namespace ECommerce.Application.Requests.Auth;

public sealed record LoginRequest(string Username, string Password);
using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/auth")]
public class AuthController(IAuthService authService) : Controller
{
    /// <summary>
    /// Login to get JWT token.
    /// </summary>
    /// <param name="request">Username and password to request</param>
    [HttpPost("request")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LoginDto> LoginAsync([FromBody] LoginRequest request)
    {
        var token = await authService.LoginAsync(request);

        return Ok(token);
    }

    /// <summary>
    /// Issues new access token based on refresh token
    /// </summary>
    /// <param name="request">Refresh token</param>
    /// <returns>New Access Token</returns>
    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
    {
        var response = await authService.RefreshTokenAsync(request);

        return Ok(response);
    }
}

using ECommerce.Application.Interfaces;
using ECommerce.Application.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/auth")]
[ApiController]

public class AuthController(IAuthService authService) : Controller
{
    /// <summary>
    /// Login to get JWT token.
    /// </summary>
    /// <param name="request">Username and password to request</param>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        try
        {
            var token = await authService.LoginAsync(request);

            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(token);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred", Error = ex.Message });
        }
    }

    /// <summary>
    /// Register to get JWT token.
    /// </summary>
    /// <param name="request">Username, password to request</param>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        try
        {
            await authService.RegisterAsync(request);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred", Error = ex.Message });
        }
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
        try
        {
            var response = await authService.RefreshTokenAsync(request);

            if (response == null)
                return Unauthorized("Invalid refresh token");

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred", Error = ex.Message });
        }
    }
}

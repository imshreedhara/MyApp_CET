using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.Interfaces;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtService;

    public AuthController(IJwtTokenService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // For demo, using hardcoded User Credentilas
        if (request.Username == "admin" && request.Password == "admin")
        {
            var user = new User { Username = "admin", Role = "Admin" };
            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        return Unauthorized();
    }
}

public class LoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

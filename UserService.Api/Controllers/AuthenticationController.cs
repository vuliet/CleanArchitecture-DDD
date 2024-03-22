using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Services.Authentication;
using UserService.Contracts.Authentication;

namespace UserService.Api.Controllers;

[Route("authentication")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(
        IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> result = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return result.Match(
            result => Ok(MapAuthenticationResult(result)),
            errors => Problem(errors));
    }

    private static AuthenticationResponse MapAuthenticationResult(AuthenticationResult result)
    {
        return new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = _authenticationService.Login(
             request.Email,
             request.Password);

        return result.Match(
            result => Ok(MapAuthenticationResult(result)),
            errors => Problem(errors));
    }
}

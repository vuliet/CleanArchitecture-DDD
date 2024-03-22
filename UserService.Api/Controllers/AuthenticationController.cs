using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Authentication.Commands;
using UserService.Application.Authentication.Common;
using UserService.Application.Authentication.Queries;
using UserService.Contracts.Authentication;

namespace UserService.Api.Controllers;

[Route("authentication")]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> result = await _mediator.Send(
            new RegisterCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password));

        return result.Match(
            result => Ok(MapAuthenticationResult(result)),
            Problem);
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
    public async Task<IActionResult> Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> result = await _mediator.Send(
            new LoginQuery(
                request.Email,
                request.Password));

        return result.Match(
            result => Ok(MapAuthenticationResult(result)),
            Problem);
    }
}

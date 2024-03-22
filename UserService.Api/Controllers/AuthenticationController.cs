using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Authentication.Commands.Register;
using UserService.Application.Authentication.Common;
using UserService.Application.Authentication.Queries;
using UserService.Contracts.Authentication;

namespace UserService.Api.Controllers;

[Route("authentication")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        ErrorOr<AuthenticationResult> result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<AuthenticationResponse>(result)),
            Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        ErrorOr<AuthenticationResult> result = await _mediator.Send(query);

        return result.Match(
            result => Ok(_mapper.Map<AuthenticationResponse>(result)),
            Problem);
    }
}

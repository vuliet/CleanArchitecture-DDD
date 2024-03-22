using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Api.Controllers;

[Route("[controller]")]
[Authorize]
public class UserController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public UserController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> MyUserProfile()
    {
        await Task.CompletedTask;
        return Ok(true);
    }
}

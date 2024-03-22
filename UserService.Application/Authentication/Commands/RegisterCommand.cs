using ErrorOr;
using MediatR;
using UserService.Application.Authentication.Common;

namespace UserService.Application.Authentication.Commands
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}

using ErrorOr;
using MediatR;
using UserService.Application.Authentication.Common;

namespace UserService.Application.Authentication.Queries
{
    public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}

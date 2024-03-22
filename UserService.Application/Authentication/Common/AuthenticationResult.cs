using UserService.Domain.Entities;

namespace UserService.Application.Authentication.Common
{
    public record AuthenticationResult(User User, string Token);
}

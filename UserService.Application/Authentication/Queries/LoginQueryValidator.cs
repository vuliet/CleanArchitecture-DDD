using FluentValidation;
using UserService.Application.Authentication.Queries;

namespace UserService.Application.Authentication.Commands.Register
{
    public class LoginQueryValidator :
        AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

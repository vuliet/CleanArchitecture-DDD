using ErrorOr;
using MediatR;
using UserService.Application.Authentication.Common;
using UserService.Application.Common.Interfaces.Authentication;
using UserService.Application.Common.Interfaces.Persistence;
using UserService.Domain.Common.Errors;
using UserService.Domain.Entities;

namespace UserService.Application.Authentication.Queries
{
    public class LoginQueryHandler :
        IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        private readonly IUserRepository _userRepository;
        public LoginQueryHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(
            LoginQuery query,
            CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (_userRepository.GetUserByEmail(query.Email) is not User user)
                return Errors.Authentication.InvalidCredentials;

            if (user.Password != query.Password)
                return new[] { Errors.Authentication.InvalidCredentials };

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}

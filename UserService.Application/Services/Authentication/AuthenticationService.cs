using UserService.Application.Common.Errors;
using UserService.Application.Common.Interfaces.Authentication;
using UserService.Application.Common.Interfaces.Persistence;
using UserService.Domain.Entities;

namespace UserService.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        private readonly IUserRepository _userRepository;
        public AuthenticationService(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not User user)
                throw new Exception("User with given email already exists.");

            if (user.Password != password)
                throw new Exception("Invalid password.");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not null)
                throw new DuplicateEmailException();

            var user = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password
            };

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}

using Mapster;
using UserService.Application.Authentication.Commands;
using UserService.Application.Authentication.Common;
using UserService.Application.Authentication.Queries;
using UserService.Contracts.Authentication;

namespace UserService.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}

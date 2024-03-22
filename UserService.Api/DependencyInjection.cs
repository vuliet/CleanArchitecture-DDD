
using Microsoft.AspNetCore.Mvc.Infrastructure;
using UserService.Api.Common.Mapping;
using UserService.Api.Errors;

namespace UserService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, UserServiceProblemDetailsFactory>();
            services.AddMappings();

            return services;
        }
    }
}

using ApiAuth.Context;
using ApiAuth.Repository;
using ApiAuth.services.user;
using Domain.Interfaces.IServices;

namespace ApiAuth.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<MyContext>();

            //Repository
            services.AddScoped<IUserRepository, UserRepository>();

            //Service
            services.AddScoped<IUserService, UserService>();
            

            services.AddHttpClient();
        }
    }
}

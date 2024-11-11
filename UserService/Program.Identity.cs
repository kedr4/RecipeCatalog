using Microsoft.AspNetCore.Identity;
using UserService.Domain.Users;
using UserService.Infrastructure.Database;

namespace UserService.Api
{
    public static class ProgramIdentity

    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole>()
                        .AddEntityFrameworkStores<DatabaseContext>()
                        .AddDefaultTokenProviders();

            return services;
        }
    }
}

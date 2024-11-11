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

            // Настройка валидации пароля для Identity
            services.Configure<IdentityOptions>(options =>
            {
                // НЕ Требовать хотя бы одну цифру в пароле
                options.Password.RequireDigit = false;

                // Требовать хотя бы одну строчную букву
                options.Password.RequireLowercase = true;

                // НЕ Требовать хотя бы одну заглавную букву
                options.Password.RequireUppercase = false;

                // НЕ Требовать хотя бы один небуквенно-цифровой символ (например, @, #, !)
                options.Password.RequireNonAlphanumeric = false;

                // Минимальная длина пароля (например, 8 символов)
                options.Password.RequiredLength = 6;

                // Требовать хотя бы один уникальный символ в пароле
                options.Password.RequiredUniqueChars = 1; // Минимум 1 уникальный символ
            });

            return services;
        }
    }
}

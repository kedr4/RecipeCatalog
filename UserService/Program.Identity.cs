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

            // ��������� ��������� ������ ��� Identity
            services.Configure<IdentityOptions>(options =>
            {
                // �� ��������� ���� �� ���� ����� � ������
                options.Password.RequireDigit = false;

                // ��������� ���� �� ���� �������� �����
                options.Password.RequireLowercase = true;

                // �� ��������� ���� �� ���� ��������� �����
                options.Password.RequireUppercase = false;

                // �� ��������� ���� �� ���� ����������-�������� ������ (��������, @, #, !)
                options.Password.RequireNonAlphanumeric = false;

                // ����������� ����� ������ (��������, 8 ��������)
                options.Password.RequiredLength = 6;

                // ��������� ���� �� ���� ���������� ������ � ������
                options.Password.RequiredUniqueChars = 1; // ������� 1 ���������� ������
            });

            return services;
        }
    }
}

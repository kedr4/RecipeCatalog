
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Database;

namespace UserService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;

            builder.Services.AddSwagger();
            builder.Services.AddControllers();
            builder.Services.AddIdentity();
            builder.Services.AddAuthentication(configuration);
            builder.Services.AddAuthorization();
            builder.Services.AddServices();
            builder.Services.AddValidation();
            builder.Services.AddEntityFramework(configuration);
            var app = builder.Build();

            app.MapControllers();
            app.UseSwagger(app);
            app.UseMiddlewares();
            app.UseAuthentication();
            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                db.Database.Migrate();

            }
            app.Run();
        }
    }
}

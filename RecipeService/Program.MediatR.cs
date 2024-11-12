using MediatR;
using System.Reflection;
using RecipeService.Api;

namespace RecipeService
{
    public static class ProgramMediatR
    {
        private static string ApplicationProjectNameSpace = $"{nameof(RecipeService)}.{nameof(Application)}";
        private static string InfrastructureProjectNameSpace = $"{nameof(RecipeService)}.{nameof(Infrastructure)}";
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services
                    .AddMediatR(o => o.RegisterServicesFromAssemblyContaining(typeof(Program)))
                    .AddPipelineBehavior()
                    .AddDomainHandlers();
        }

        private static IServiceCollection AddDomainHandlers(this IServiceCollection services)
        {

            return services.Scan(scan =>
            {
                scan
                    .FromAssemblies(Assembly.Load(ApplicationProjectNameSpace), Assembly.Load(InfrastructureProjectNameSpace))
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IRequestHandler<>)))
                    .AsImplementedInterfaces()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IRequestHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

        }
        private static IServiceCollection AddPipelineBehavior(this IServiceCollection services)
        {

            return services.Scan(scan =>
            {
                scan
                    .FromAssemblies(Assembly.Load(ApplicationProjectNameSpace))
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IPipelineBehavior<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

        }
    }
}

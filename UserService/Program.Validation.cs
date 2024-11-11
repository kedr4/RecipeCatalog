namespace UserService.Api
{
    public static class ProgramValidation
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            //services.AddScoped<IValidator<CreateUserRequest>,CreateUserRequestValidators>();
            //services.AddScoped<IValidator<UpdateUserRequest>, UpdateProductRequestValidator>();
            //services.AddFluentValidationAutoValidation();
            return services;
        }

    }
}

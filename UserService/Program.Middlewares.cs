namespace UserService.Api
{
    public static class ProgramMiddlewares
    {
        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        {
            return app;
        }
    }
}

namespace Solvintech.API.Extensions.Core
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseWebApi(this IApplicationBuilder app)
        {
            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            return app;
        }
    }
}

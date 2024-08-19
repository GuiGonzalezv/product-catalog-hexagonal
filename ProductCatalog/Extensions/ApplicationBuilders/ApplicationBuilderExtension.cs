using ProductCatalog.Middleware;

namespace ProductCatalog.Extensions.ApplicationBuilders
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder AddApplicationBuilder(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseRouting();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseEndpoints(endpoint => { 
                endpoint.MapControllers();
                endpoint.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            return app;
        }
    }
}

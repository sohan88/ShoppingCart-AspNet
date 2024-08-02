using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ShoppingCart.Data;

namespace ShoppingCart
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the ApplicationDbContext with an in-memory database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("ShoppingCartDb"));

            // Register the MVC services
            services.AddControllers();
            
            // Register the Swagger services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Shopping Cart API",
                    Description = "An API for managing a shopping cart"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping Cart API V1");
                c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
            });
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
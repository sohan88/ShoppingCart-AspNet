using ShoppingCart.Data;
using ShoppingCart.Models;

namespace ShoppingCart
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateBuilder(args).Build().Run();
            
            //See Db with Dummy Data
            // using (var scope = host.Services.CreateScope())
            // {
            //     var services = scope.ServiceProvider;
            //     var context = services.GetRequiredService<ApplicationDbContext>();
            //     if (!context.Products.Any())
            //     {
            //         context.Products.Add(new Product { Title = "Corn Flakes", Price = 2.52m });
            //         context.Products.Add(new Product { Title = "Milk", Price = 1.15m });
            //         context.SaveChanges();
            //     }
            // }
          //  host.Run();
        }

        private static IHostBuilder CreateBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
    
}
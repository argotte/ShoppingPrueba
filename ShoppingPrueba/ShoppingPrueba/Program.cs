using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shooping.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingPrueba
{
    public class Program
    {
        public static void Main(string[] args)
        {
 //           CreateHostBuilder(args).Build().Run();
            IWebHost host = CreateWebHostBuilder(args).Build();
            RunSeeding(host);
            host.Run();
        }
        //Metodo para poder llamar nuestro seeder manualmente y poder crear la Bd si no existe cuando iniciamos el proyecto
        private static void RunSeeding(IWebHost host)
        {
            IServiceScopeFactory scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                SeedDb seeder = scope.ServiceProvider.GetService<SeedDb>();
                seeder.SeedAsync().Wait();  //El .Wait es para esperar ya que el metodo RunSeeding no es Asincrono
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using RESTfulBankApplication.Domain;
using RESTfulBankApplication.Domain.Infrastructure;

namespace RESTfulBankApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            SeedDataBase(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SeedDataBase(IHost host)
        {
            try
            {
                var context = host.Services.CreateScope().ServiceProvider.GetService<BankContext>();
                DbInitializer.SeedDataBase(context);
            }
            catch (Exception exception)
            {
                var logger = host.Services.CreateScope().ServiceProvider.GetService<ILogger<Program>>();
                logger.LogError(exception, $"An error occurred while seeding the database with message: {exception.Message}");
            }
        }
    }
}

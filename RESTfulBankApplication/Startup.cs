using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RESTfulBankApplication.Domain;

namespace RESTfulBankApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JsonOptions>((_) => new JsonSerializerOptions() {IgnoreNullValues = true});

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RESTfulBankApplication", Version = "v1" });
            });

            services.AddDbContext<BankContext>(
                x =>
                {
                    x.UseSqlServer(Configuration.GetConnectionString("Default"));
                    x.UseLoggerFactory(LoggerFactory.Create(builder =>
                        builder
                            .SetMinimumLevel(LogLevel.Information)
                            .AddFilter((category, level) => 
                                (category == DbLoggerCategory.Query.Name || 
                                 category == DbLoggerCategory.Database.Connection.Name || 
                                 category == DbLoggerCategory.Database.Command.Name))
                            .AddConsole()));

                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTfulBankApplication v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

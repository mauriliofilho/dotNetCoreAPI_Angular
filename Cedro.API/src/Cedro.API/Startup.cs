using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Cedro.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Cedro.API.Services;

namespace Cedro.API
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionstring = Startup.Configuration["connectionstring:connectContext"]; 
            services.AddDbContext<ConnectContext>(o => o.UseSqlServer(connectionstring));

            services.AddScoped<IRestauranteInfoRepository, RestauranteInfoRepository>();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ConnectContext connectContext)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            connectContext.EnsureSeedDataForContext();
            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Restaurantes, Models.RestaurantesSemPratosDto>();
                cfg.CreateMap<Entities.Restaurantes, Models.RestauranteDto>();
                cfg.CreateMap<Models.RestauranteForUpdateDto, Entities.Restaurantes>();
                cfg.CreateMap<Entities.PratosRestaurantes, Models.PratosRestaurantesDto>();
                cfg.CreateMap<Models.PratosRestaurantesDto, Entities.PratosRestaurantes>();
                cfg.CreateMap<Models.PratosRestaurantesForUpdateDto, Entities.PratosRestaurantes>();
                cfg.CreateMap<Entities.Restaurantes, Models.RestauranteDto>();

            });

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("API Ready!!!!!");
            });
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using TheWorld.Models;
using TheWorld.Services;

namespace TheWorld
{
  public class Startup
  {
    private IHostingEnvironment _env;
    private IConfigurationRoot _config;

    public Startup(IHostingEnvironment env)
    {
      _env = env;

      var builder = new ConfigurationBuilder()
        .SetBasePath(_env.ContentRootPath)
        .AddJsonFile("config.json")
        .AddEnvironmentVariables();

      _config = builder.Build();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddDbContext<WorldContext>();

      services.AddSingleton(_config);

      services.AddTransient<WorldContextSeedData>();//every time transient gonna create data...worldseeddata is data class. 

      services.AddTransient<GeoCoordsService>();

      services.AddScoped<IWorldRepository, WorldRepository>();

        services.AddIdentity<WorldUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";

            })
            .AddEntityFrameworkStores<WorldContext>();
        services.AddLogging();
            

            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
      {
        services.AddScoped<IMailService, DebugMailService>();
      
      }
      else
      {
        // Implement a real Mail Service
      }

            //services.AddMvc(); //just for using mvc 
            services.AddMvc(config =>
                {
                    if (_env.IsProduction()) { 
                    config.Filters.Add(new RequireHttpsAttribute());// for overall post to hide credentials in production mode
                    }

                })
                .AddJsonOptions(confiq =>
                {
                    confiq.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); //for showing json in camel case for api 
            });
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env,WorldContextSeedData seeder,ILoggerFactory factory)
    {
       Mapper.Initialize(config =>
       {
           config.CreateMap<TripViewModel, Trip>().ReverseMap();//used for automapper for data mapper and reverse map is to from entity to view model ,two ways data traveling
           config.CreateMap<StopsViewModel, Stop>().ReverseMap();
       });

        app.UseStaticFiles();

        app.UseIdentity();

      if (env.IsEnvironment("Development"))
      {
        app.UseDeveloperExceptionPage();
          factory.AddDebug(LogLevel.Information);
      }
      else
      {
          factory.AddDebug(LogLevel.Error);
      }

      

      app.UseMvc(config =>
      {
        config.MapRoute(
          name: "Default",
          template: "{controller}/{action}/{id?}",
          defaults: new { controller = "App", action = "Index" }
          );
      });

        seeder.EnsureSeedData().Wait();
    }
  }
}

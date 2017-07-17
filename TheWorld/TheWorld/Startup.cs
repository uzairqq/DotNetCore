using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TheWorld
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            //app.UseDefaultFiles(); // to serve the files and page in entring the localhost and not doing with doing like eg:- http://localhost:0000/index.html

            app.UseStaticFiles(); //this is the middleWare to serve static files like html,javascript and css

            app.UseMvc(config => config.MapRoute( //routes 
                name: "Default",
                template: "{controller}/{action}/{id?}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",

                }

                ));
        }
    }
}

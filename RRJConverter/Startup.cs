using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RRJConverter.Models;
using RRJConverter.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RRJConverter
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
            services.AddControllers();
            services.AddTransient<JsonListOfValutesService>();
            services.AddTransient<ConverterService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            

            app.UseRouting();

            app.MapWhen(context => {      
                return !Decimal.TryParse(context.Request.Query["count"], NumberStyles.Any, CultureInfo.InvariantCulture, out _);
            }, UncorrectURL);


            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            static void UncorrectURL(IApplicationBuilder app)
            {
                app.Run(async context =>
                {
                    ErrorResponseModel errorResponse = new ErrorResponseModel
                    {
                        Error = "Error. Check given data"
                    };
                    await context.Response.WriteAsync(errorResponse.GetErrorRespose());

                });
            }
        }
    }
}

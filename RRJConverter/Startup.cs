using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RRJConverter.Models;
using RRJConverter.Models.DatabaseModels;
using RRJConverter.Services;


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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));
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

            //app.MapWhen(context => {      
            //    return !Decimal.TryParse(context.Request.Query["count"], NumberStyles.Any, CultureInfo.InvariantCulture, out _);
            //}, UncorrectURL);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            static void UncorrectURL(IApplicationBuilder app)
            {
                app.Run(async context =>
                {
                    ErrorResponseModel errorResponse = new ErrorResponseModel();
                    await context.Response.WriteAsync(errorResponse.GetErrorResponse("Error. Check given data"));
                });
            }
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API
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
            /*Register the Swagger generator-start*/
            //services.AddSwaggerGen();
            services.AddSwaggerGen(
                (options)=> 
                {
                    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
                    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v2" });
                }
                );
            /*Register the Swagger generator-end*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*Enable middleware to serve generated Swagger as a JSON endpoint-start*/
            app.UseSwagger();
            /*Enable middleware to serve generated Swagger as a JSON endpoint-end*/
            /*specify the Swagger JSON endpoint-start*/
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API v2");
            });
            /*specify the Swagger JSON endpoint-end*/
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

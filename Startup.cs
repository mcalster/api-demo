using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Prometheus;
// dotnet add package NSwag.AspNetCore
using NJsonSchema;
using NSwag.AspNetCore;

// dotnet add package Swashbuckle.AspNetCore
//using Swashbuckle.AspNetCore.Swagger;

namespace api_demo
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHealthChecks();
            services.AddSwaggerDocument();
/*            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new Info { Title = "My Value API", Version = "v1" });
           });
 */       }
 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMetricServer("/prometheus");
            app.UseHealthChecks("/health"); //,port: 8081
            //app.UseHttpsRedirection();
            app.UseMvc();
            // Code first
            app.UseSwagger();
            //app.UseSwaggerUi3();
            app.UseReDoc( );

            // Contract First
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>{ c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");});
            //app.UseSwaggerUI();
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaliciaSegurosReference;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Product_Management_Core.Services.Impl;
using Product_Management_Core.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace SetupBusines
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
            services.AddOptions();
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Bill Busines API", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
                c.DescribeAllEnumsAsStrings();
            });

            services.AddScoped<IProductManagementService, ProductManagementService>();

            services.AddScoped<IManager>(provider =>
            {
                var client = new ManagerClient();

                return client;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );

            //app.UseErrorHandling();
            app.UseStaticFiles();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Host = httpReq.Host.Value;
                    swaggerDoc.BasePath = "/";
                    swaggerDoc.Schemes = new List<string>() { "http", "https" };
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bill Busines API");
            });

            app.UseMvc();
        }
    }
}

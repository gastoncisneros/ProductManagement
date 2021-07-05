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
using Product_Management.Middlewares;
using Product_Management_Core.Exceptions;
using Product_Management_Core.Services.Impl;
using Product_Management_Core.Services.Interfaces;
using Product_Management_Shared.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Product_Management
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

            services.Configure<MongoDbOptions>(Configuration.GetSection("MognoDbOptions"));

            services.Configure<PhotosOptions>(Configuration.GetSection("MandatoryPhoto:0"));

            services.Configure<ProductManagementOptions>(config => 
            {
                config.OnDemandOptions = Configuration.GetSection("OnDemandOptions").Get<OnDemandOptions>();
            });

            services.Configure<ProductManagementOptions>(Configuration.GetSection("ProductManagementOptions"));
            services.Configure<ProductManagementOptionsArgentina>(Configuration.GetSection("ProductManagementOptionsArgentina"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Product Management API", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
                c.DescribeAllEnumsAsStrings();
            });

            #region Services

            services.AddGeneralServices();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddAzureWebAppDiagnostics();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );

            app.UseErrorHandling();
            app.UseStaticFiles();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Host = httpReq.Host.Value;
                    swaggerDoc.BasePath = "/";
                    swaggerDoc.Schemes = new List<string>() { "https", "http" };
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Management API");
            });

            app.UseMvc();
        }
    }
}

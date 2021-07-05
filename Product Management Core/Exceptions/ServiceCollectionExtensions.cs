using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Product_Management_Client;
using Product_Management_Core.ClientManager;
using Product_Management_Core.DataValidation;
using Product_Management_Core.DataValidation.Interfaces;
using Product_Management_Core.Services;
using Product_Management_Core.Services.ErrorManager;
using Product_Management_Core.Services.Impl;
using Product_Management_Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Product_Management_Core.Exceptions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddGeneralServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly)
                .AddScoped<IVTSConnectorService, VTSConnectorService>()
                .AddScoped<IXmlService, XmlService>()
                .AddScoped<IProductManagementService, ProductManagementService>()
                .AddScoped(ClientFactory.RegisterVTSService)
                .AddScoped(ClientFactory.RegisterVTSServiceAdded)
                .AddScoped<IProductManagementValidation, ProductManagementValidation>()
                .AddScoped<IGetEnviromentBusinessValidator, GetEnviromentBusinessValidator>()
                .AddSingleton<IGetEnviromentBusinessErrorManager, GetEnviromentErrorManager>()
                .AddScoped<IBillBusinessValidator, BillBusinessValidator>()
                .AddSingleton<IBillBusinessErrorManager, BillBusinessErrorManager>()
                .AddScoped<ISetUpBusinessValidator, SetupBusinessValidator>()
                .AddSingleton<ISetupBusinessErrorManager, SetupBusinessErrorManager>()
                .AddScoped<IGetUnderWritingValidator, GetUnderWritingValidator>()
                .AddSingleton<IGetUnderWritingErrorManager, GetUnderWritingErrorManager>()
                .AddScoped<IGetEnviromentBusinessByIdValidator, GetEnviromentBusinessByIdValidator>()
                .AddSingleton<IGetEnviromentByIdErrorManager, GetEnviromentByIdErrorManager>()
                .AddScoped<IRestClientService, RestClientService>()
                .AddScoped<IRestClient, RestClient>();
        }
    }
}

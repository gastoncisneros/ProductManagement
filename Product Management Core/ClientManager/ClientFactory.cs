using GaliciaSegurosReference;
using Microsoft.Extensions.Options;
using Product_Management_Shared.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Product_Management_Core.ClientManager
{
    public interface IClientFactory { }

    public class ClientFactory
    {
        public static IManagerClient RegisterVTSService(IServiceProvider provider)
        {
            var methodOptions = (IOptions<ProductManagementOptions>)provider.GetService(typeof(IOptions<ProductManagementOptions>));

            var logger = (ILogger<object>)provider.GetService(typeof(ILogger<object>));

            var client = new ManagerClient(methodOptions.Value.WebServiceUrl, logger);
            return client;
        }

        public static GaliciaConnectedServiceAdded.IManagerClientArgentina RegisterVTSServiceAdded(IServiceProvider provider)
        {
            var methodOptions = (IOptions<ProductManagementOptionsArgentina>)provider.GetService(typeof(IOptions<ProductManagementOptionsArgentina>));

            var logger = (ILogger<object>)provider.GetService(typeof(ILogger<object>));

            var client = new GaliciaConnectedServiceAdded.ManagerClient(methodOptions.Value.WebServiceUrlArgentina, logger);
            return client;

        }
    }
}

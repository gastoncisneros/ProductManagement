using Product_Management_Client.Logging;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GaliciaConnectedServiceAdded
{
    public interface IManagerClientArgentina
    {
        Task<UnderwritingCaseStatusResultCollection> GetUnderwritingStatusAsync(UnderwritingCaseStatusCollection underwritingCaseStatus);

        Task<string> HealthCheckAsync();
    }

    public partial class ManagerClient : IManagerClientArgentina
    { 
        private static string _endpoint;
        private static ILogger<object> _logger;

        public ManagerClient(string endpoint, ILogger<object> logger) : base(GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IManager),
            GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IManager))
        {
            _endpoint = endpoint;
            _logger = logger;
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        static partial void ConfigureEndpoint(ServiceEndpoint serviceEndpoint, ClientCredentials clientCredentials)
        {
            if (!string.IsNullOrEmpty(_endpoint))
            {
                _logger.LogCritical("ENDPOINT CONFIGURADO: " + _endpoint);
                serviceEndpoint.Address = new EndpointAddress(_endpoint);
                serviceEndpoint.EndpointBehaviors.Add(new LoggingBehaviour(_logger));
                if (_endpoint.StartsWith("https"))
                    (serviceEndpoint.Binding as BasicHttpBinding).Security.Mode = BasicHttpSecurityMode.Transport;
            }
        }
    }
}

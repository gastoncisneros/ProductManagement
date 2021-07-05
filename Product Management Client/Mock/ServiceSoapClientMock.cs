using GaliciaSegurosReference;
using Product_Management_Shared.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_Client.Mock
{
    public class ServiceSoapClientMock : IManagerClient
    {

        private readonly ProductManagementOptions _webServiceOptions;

        public ServiceSoapClientMock(ProductManagementOptions webServiceOptions)
        {
            _webServiceOptions = webServiceOptions;
        }

        public Task<NewBusinessProposal> BillBusinessAsync(NewBusinessProposal request)
        {
            return Task.Run(() => new NewBusinessProposal());
        }

        public Task<CommercialStructuresCollection> GetEnvironmentBusinessAsync(DateTime effectiveDate, int firstComponentLevel, int secondComponentLevel, int thirdComponentLevel)
        {
            return Task.Run(() => new CommercialStructuresCollection());
        }

        public Task<CommercialStructures> GetEnvironmentBusinessByIdAsync(DateTime effectiveDate, int commercialStructureId)
        {
            throw new NotImplementedException();
        }

        public Task<NewBusinessProposal> SetUpBusinessAsync(NewBusinessProposal request, int externalUserId)
        {
            throw new NotImplementedException();
        }

        public Task<string> HealthCheckAsync()
        {
            throw new NotImplementedException();
        }
    }
}

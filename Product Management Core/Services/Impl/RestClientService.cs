using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Product_Management_Client;
using Product_Management_Core.DTO.OnDemand;
using Product_Management_Core.DTO.SetupUnderWrittingAndPolicyDoc;
using Product_Management_Domain.Entities;
using Product_Management_Shared.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_Core.Services.Impl
{
    public interface IRestClientService
    {
        Task<BaseResponse> GetPolicyDoc(GetPolicyDocRequest request);
    }

    public class RestClientService : IRestClientService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RestClientService> _logger;
        private readonly IRestClient _restClient;
        private readonly ProductManagementOptions _options;
        private readonly OnDemandOptions _onDemandOptions;

        public RestClientService(IMapper mapper,
                                 ILogger<RestClientService> logger,
                                 IRestClient restClient,
                                 IOptions<ProductManagementOptions> options)
        {
            _mapper = mapper;
            _logger = logger;
            _restClient = restClient;
            _options = options.Value;
            _onDemandOptions = options.Value.OnDemandOptions;
        }

        public async Task<BaseResponse> GetPolicyDoc(GetPolicyDocRequest request)
        {
            var polizaProd = request.CertificateID == 0 ? 0 : request.PolicyId;
            var onDemandRequest = new OnDemandRequest
            {
                method = _onDemandOptions.OnDemandMethod,
                credentials = _onDemandOptions.OnDemandCreds,
                modulo = "1",
                ts_user_id = "APIPDF",
                polizaProd = polizaProd.ToString(),
                dStArtDate = request.Date?.ToString("yyyyMMdd") ?? DateTime.Today.ToString("yyyyMMdd"),
                sCertype = "2",
                cLote = "RMP",
                nOriginCode = "1",
                nLetTernum = "0"
            };
            _mapper.Map(request, onDemandRequest);

            _logger.LogDebug("OnDemandRequest: " + JsonConvert.SerializeObject(onDemandRequest));

            var onDemandResponse = await _restClient.PostAsyncToString(_onDemandOptions.OnDemandUrl, onDemandRequest);

            _logger.LogDebug("OnDemandResponse: " + onDemandResponse);

            return ParseResponseFromOnDemand(onDemandResponse);
        }

        private BaseResponse ParseResponseFromOnDemand(string responseJson)
        {
            // El servicio responde en 2 formatos.
            // Formato de ErrorResponse (error y codigo de error)
            // o solo el Base64 del pdf que trajo

            try { return JsonConvert.DeserializeObject<OnDemandError>(responseJson); }
            catch (Exception) { }

            try { return new OnDemandResponse { Base64 = responseJson }; }
            catch (Exception) { }

            throw new Exception(string.Format("Cannot parse response: {0}", responseJson));
        }
    }
}

using AutoMapper;
using GaliciaSegurosReference;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Product_Management_Client;
using Product_Management_Core.DTO;
using Product_Management_Core.DTO.GetEnviromentBusiness;
using Product_Management_Core.DTO.GetUnderWriting;
using Product_Management_Core.DTO.HealthCheck;
using Product_Management_Core.DTO.OnDemand;
using Product_Management_Core.DTO.SetupAndGetUnderWritting;
using Product_Management_Core.DTO.SetupUnderWrittingAndPolicyDoc;
using Product_Management_Core.Exceptions;
using Product_Management_Core.Services.Impl;
using Product_Management_Domain.Entities;
using Product_Management_Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_Core.Services
{
    public interface IVTSConnectorService
    {
        Task<GetEnviromentResponse> GetEnviromentBusines(GetEnviromentRequest request);

        Task<GetEnviromentByIdResponse> GetEnviromentBusinessById(GetEnviromentRequestById request);

        Task<BillBusinessResponse> BillBusiness(GetBillBusinesRequest request);

        Task<BillBusinessResponse> SetupBusiness(GetBillBusinesRequest request);

        Task<GetUnderWritingResponse> GetUnderWrittingStatus(GaliciaConnectedServiceAdded.UnderwritingCaseStatusCollection collection);

        Task<SetupAndGetUnderWrittingResponse> SetupAndGetUnderWritting(GetBillBusinesRequest request);

        Task<SetupAndGetUnderAndPolicyResponse> SetupGetUnderAndPolicy(GetBillBusinesRequest request);

        Task<BaseResponse> HealthCheck();
    }

    public class VTSConnectorService : IVTSConnectorService
    {
        private readonly IMapper _mapper;
        private readonly IXmlService _xmlService;
        private readonly IManagerClient _vtsSoapService;
        private readonly GaliciaConnectedServiceAdded.IManagerClientArgentina _vtsSoapServiceArgentina;
        private readonly ILogger<VTSConnectorService> _logger;
        private readonly ProductManagementOptions _options;
        private readonly ProductManagementOptionsArgentina _optionsAdded;
        private readonly IRestClientService _restClientService;

        public VTSConnectorService(IMapper mapper
            , IOptions<ProductManagementOptions> options
            , IOptions<ProductManagementOptionsArgentina> optionsAdded
            , IXmlService xmlService
            , IManagerClient vtsSoapService
            , ILogger<VTSConnectorService> logger
            , GaliciaConnectedServiceAdded.IManagerClientArgentina vtsSoapServiceArgentina
            , IRestClientService restClientService)
        {
            _mapper = mapper;
            _vtsSoapService = vtsSoapService;
            _logger = logger;
            _options = options.Value;
            _xmlService = xmlService;
            _vtsSoapServiceArgentina = vtsSoapServiceArgentina;
            _optionsAdded = optionsAdded.Value;
            _restClientService = restClientService;
        }

        public NewBusinessProposal ArmarRequest(GetBillBusinesRequest request)
        {
            var response = new NewBusinessProposal
            {
                Billing = new Billing
                {
                    BillingEndDate = DateTime.MinValue.AddDays(4),
                    BillingStartDate = DateTime.MinValue.AddDays(4),
                    BillingTotalCommission = 0,
                    BillingTotalPremium = 0
                },
                Risk = request.Risk,
                UnderwrittingCaseId = request.UnderwrittingCaseId,
                ThreadRoutines = 2,
                Errors = new string[0]
            };
            response.Risk.ParticularData.TrasactionId = 1;

            return response;
        }

        public async Task<BillBusinessResponse> BillBusiness(GetBillBusinesRequest request)
        {
            var requestEnviado = _xmlService.Serialize(request);

            _logger.LogDebug("Request Enviado: " + requestEnviado);
            _logger.LogDebug("Endpoint VTS generado: " + _options.WebServiceUrl);

            var toSend = ArmarRequest(request);

            var billBusiness = new NewBusinessProposal();

            try
            {
                billBusiness = await _vtsSoapService.BillBusinessAsync(toSend);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            var respuesta = _xmlService.Serialize(billBusiness);
            _logger.LogDebug("Respuesta: " + respuesta);

            BillBusinessResponse result = new BillBusinessResponse {
                Billing = billBusiness.Billing,
                UnderwrittingCaseId = billBusiness.UnderwrittingCaseId,
                ThreadRoutines = 2,
                Risk = billBusiness.Risk,
                IsSuccess = true };

            return result;
        }

        public async Task<GetEnviromentResponse> GetEnviromentBusines(GetEnviromentRequest request)
        {
            var requestEnviado = _xmlService.Serialize(request);

            _logger.LogDebug("Request Enviado: " + requestEnviado);
            _logger.LogDebug("Endpoint VTS generado: " + _options.WebServiceUrl);

            var commercialStructures = await _vtsSoapService.GetEnvironmentBusinessAsync(request.effectiveDate, request.firstComponentLevel, request.secondComponentLevel, request.thirdComponentLevel);

            var respuesta = _xmlService.Serialize(commercialStructures);
            _logger.LogDebug("Respuesta: " + respuesta);

            var response = commercialStructures.Select(x => _mapper.Map<CommercialStructures, Response>(x)).ToArray();

            GetEnviromentResponse result = new GetEnviromentResponse { Response = response, IsSuccess = true};

            return result;
        }

        public async Task<GetEnviromentByIdResponse> GetEnviromentBusinessById(GetEnviromentRequestById request)
        {
            var requestEnviado = _xmlService.Serialize(request);

            _logger.LogDebug("Request Enviado: " + requestEnviado);
            _logger.LogDebug("Endpoint VTS generado: " + _options.WebServiceUrl);

            var commercialStructures = await _vtsSoapService.GetEnvironmentBusinessByIdAsync(effectiveDate:request.effectiveDate, commercialStructureId:request.commercialStructureId);

            var respuesta = _xmlService.Serialize(commercialStructures);
            _logger.LogDebug("Respuesta: " + respuesta);

            var response = _mapper.Map<CommercialStructures, Response>(commercialStructures);

            GetEnviromentByIdResponse result = new GetEnviromentByIdResponse { Response = response, IsSuccess = true };

            return result;
        }

        public async Task<BillBusinessResponse> SetupBusiness(GetBillBusinesRequest request)
        {
            BillBusinessResponse billBusinesData = await BillBusiness(request);

            var response = _mapper.Map<BillBusinessResponse, NewBusinessProposal>(billBusinesData);
            try
            {
                response = await _vtsSoapService.SetUpBusinessAsync(response, 1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var result = new BillBusinessResponse
            {
                Billing = response.Billing,
                IsSuccess = true,
                Risk = response.Risk,
                UnderwrittingCaseId = response.UnderwrittingCaseId,
                ThreadRoutines = response.ThreadRoutines
            };

            return result;
        }

        public async Task<GetUnderWritingResponse> GetUnderWrittingStatus(GaliciaConnectedServiceAdded.UnderwritingCaseStatusCollection request)
        {
            GetUnderWritingResponse response = new GetUnderWritingResponse();
            var underWritting = new GaliciaConnectedServiceAdded.UnderwritingCaseStatusResultCollection();

            try
            {
                underWritting = await _vtsSoapServiceArgentina.GetUnderwritingStatusAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //var result = underWritting.Select(x => _mapper.Map<GaliciaConnectedServiceAdded.UnderwritingCaseStatusResult, >)

            response.Response = underWritting.Select(x => new GaliciaConnectedServiceAdded.UnderwritingCaseStatusResult
            {
                CertificateID = x.CertificateID,
                CloseDate = x.CloseDate,
                CreationDate = x.CreationDate,
                Decision = x.Decision,
                DecisionDescription = x.DecisionDescription,
                ErrorLog = x.ErrorLog,
                LineOfBusiness = x.LineOfBusiness,
                LineOfBusinessDescription = x.LineOfBusinessDescription,
                PolicyID = x.PolicyID,
                ProductCode = x.ProductCode,
                ProductDescription = x.ProductDescription,
                UnderwritingCaseId = x.UnderwritingCaseId,
                UnderwritingCaseType = x.UnderwritingCaseType,
                UnderwritingCaseTypeDescription = x.UnderwritingCaseTypeDescription
            }).ToList();
            response.IsSuccess = true;
            return response;
        }

        public async Task<SetupAndGetUnderWrittingResponse> SetupAndGetUnderWritting(GetBillBusinesRequest request)
        {
            try
            {
                int seconds = int.Parse(TimeSpan.FromSeconds(_options.DelayFunction).TotalMilliseconds.ToString());

                BillBusinessResponse responseSetup = (seconds > 0) ? await Task.Delay(seconds).ContinueWith(x => SetupBusiness(request)).Result : await SetupBusiness(request);

                GaliciaConnectedServiceAdded.UnderwritingCaseStatusCollection requestUnderWritting = new GaliciaConnectedServiceAdded.UnderwritingCaseStatusCollection
                {
                    new GaliciaConnectedServiceAdded.UnderwritingCaseStatus{UnderwritingCaseId = responseSetup.UnderwrittingCaseId }
                };


                GetUnderWritingResponse responseGet = await GetUnderWrittingStatus(requestUnderWritting);

                SetupAndGetUnderWrittingResponse response = new SetupAndGetUnderWrittingResponse
                {
                    UnderWrittingResponse = responseGet,
                    SetupResponse = responseSetup,
                    IsSuccess = true
                };

                return response;
            }
            catch (Exception ex)
            {   
                throw new Exception(ex.Message);
            }
        }

        public async Task<SetupAndGetUnderAndPolicyResponse> SetupGetUnderAndPolicy(GetBillBusinesRequest request)
        {
            GetPolicyDocRequest requestGetPolicy = new GetPolicyDocRequest();

            try
            {
                var resource = await SetupAndGetUnderWritting(request);
                requestGetPolicy.PolicyId = resource.UnderWrittingResponse.Response.Select(x => x.PolicyID).First();
                requestGetPolicy.Branch = resource.UnderWrittingResponse.Response.Select(x => x.LineOfBusiness).First();
                requestGetPolicy.Product = resource.UnderWrittingResponse.Response.Select(x => x.ProductCode).First();
                requestGetPolicy.CertificateID = resource.UnderWrittingResponse.Response.Select(x => x.CertificateID).First();
                requestGetPolicy.Date = resource.UnderWrittingResponse.Response.Select(x => x.CloseDate).First();
                requestGetPolicy.DocNumber = "30456789";
                requestGetPolicy.Sexo = "2";
                requestGetPolicy.TipoDoc = "5";
                requestGetPolicy.Nombre = "Usuario";
                requestGetPolicy.Apellido = "Test";
                requestGetPolicy.Email = "usuariotest@hotmail.com";
                requestGetPolicy.Origen = "VT6";

               var policyResp = await _restClientService.GetPolicyDoc(requestGetPolicy);

                SetupAndGetUnderAndPolicyResponse response = new SetupAndGetUnderAndPolicyResponse();
                response.Base64 = ((OnDemandResponse)policyResp)?.Base64;
                response.Errors = resource.Errors;
                response.SetupResponse = resource.SetupResponse;
                response.UnderWrittingResponse = resource.UnderWrittingResponse;
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BaseResponse> HealthCheck()
        {
            HealthCheckResponse healthCheck = new HealthCheckResponse();

            try
            {
                healthCheck.Respuesta_Manager = await _vtsSoapService.HealthCheckAsync();
                healthCheck.Respuesta_Argentina = await _vtsSoapServiceArgentina.HealthCheckAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }

            var respuesta1 = _xmlService.Serialize(healthCheck.Respuesta_Manager);
            var respuesta2 = _xmlService.Serialize(healthCheck.Respuesta_Argentina);
            _logger.LogDebug(string.Format("Respuesta 1: {0}, Respuesta 2: {1}", respuesta1, respuesta2));

            return ParseResponse(healthCheck);
        }

        private BaseResponse ParseResponse(HealthCheckResponse response)
        {
            try { return JsonConvert.DeserializeObject<HealthCheckError>(response.Respuesta_Manager); }
            catch (Exception) { }

            try { return JsonConvert.DeserializeObject<HealthCheckError>(response.Respuesta_Argentina); }
            catch (Exception) { }

            try { return new HealthCheckResponse { Respuesta_Manager = response.Respuesta_Manager, Respuesta_Argentina = response.Respuesta_Argentina, Errors = response.Errors}; }
            catch (Exception) { }

            throw new Exception(string.Format("Cannot parse response: {0}", response.Respuesta_Manager));

            throw new Exception(string.Format("Cannot parse response: {0}", response.Respuesta_Argentina));
        }
    }
}

using AutoMapper;
using GaliciaConnectedServiceAdded;
using GaliciaSegurosReference;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Product_Management_Core.DataValidation.Attributes;
using Product_Management_Core.DataValidation.Interfaces;
using Product_Management_Core.DTO;
using Product_Management_Core.DTO.GetEnviromentBusiness;
using Product_Management_Core.DTO.GetUnderWriting;
using Product_Management_Core.DTO.HealthCheck;
using Product_Management_Core.DTO.SetupAndGetUnderWritting;
using Product_Management_Core.Exceptions;
using Product_Management_Core.Services.Interfaces;
using Product_Management_Domain.Entities;
using Product_Management_Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_Core.Services.Impl
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IVTSConnectorService _vtsConectorService;
        private readonly IGetEnviromentBusinessValidator _getEnviromentBusinessValidator;
        private readonly IBillBusinessValidator _billBusinessValidator;
        private readonly ISetUpBusinessValidator _setUpBusinessValidator;
        private readonly IGetUnderWritingValidator _getUnderWritingValidator;
        private readonly IGetEnviromentBusinessByIdValidator _getEnviromentBusinessByIdValidator;
        private readonly IRestClientService _restClientService;
        private readonly IOptions<MongoDbOptions> _mongoDbOptions;
        private readonly IOptions<PhotosOptions> _photosOptions;

        public ProductManagementService(IVTSConnectorService vtsConnectorService,
                                     IGetEnviromentBusinessValidator getEnviromentBusinessValidator,
                                     IBillBusinessValidator billBusinessValidator,
                                     ISetUpBusinessValidator setUpBusinessValidator,
                                     IGetUnderWritingValidator getUnderWritingValidator,
                                     IGetEnviromentBusinessByIdValidator getEnviromentBusinessByIdValidator,
                                     IRestClientService restClientService,
                                     IOptions<MongoDbOptions> mongoDbOptions,
                                     IOptions<PhotosOptions> photosOptions)
        {
            _vtsConectorService = vtsConnectorService;
            _getEnviromentBusinessValidator = getEnviromentBusinessValidator;
            _billBusinessValidator = billBusinessValidator;
            _setUpBusinessValidator = setUpBusinessValidator;
            _getUnderWritingValidator = getUnderWritingValidator;
            _getEnviromentBusinessByIdValidator = getEnviromentBusinessByIdValidator;
            _restClientService = restClientService;
            _mongoDbOptions = mongoDbOptions;
            _photosOptions = photosOptions;
        }

        public async Task<GetEnviromentResponse> GetEnviromentBusines(GetEnviromentRequest request, ModelStateDictionary modelState)
        {
            GetEnviromentResponse response = new GetEnviromentResponse();

            response.Errors = _getEnviromentBusinessValidator.Validate(request, modelState).ToArray();

            if (response.Errors.Count() == 0)
            {
                response = await _vtsConectorService.GetEnviromentBusines(request);
                response.IsSuccess = true;
            }
            else
            {
                if (response.Errors != null && response.Errors.Count() != 0) throw new ProductException(response);
            }

            return response;
        }

        public async Task<GetEnviromentByIdResponse> GetEnviromentBusinesById(GetEnviromentRequestById request, ModelStateDictionary modelState)
        {
            GetEnviromentByIdResponse response = new GetEnviromentByIdResponse();

            response.Errors = _getEnviromentBusinessByIdValidator.Validate(request, modelState).ToArray();

            if (response.Errors.Count() == 0)
            {
                response = await _vtsConectorService.GetEnviromentBusinessById(request);
                response.IsSuccess = true;
            }
            else
            {
                if (response.Errors != null && response.Errors.Count() != 0) throw new ProductException(response);
            }
            return response;
        }

        public async Task<BillBusinessResponse> GetBillBusines(GetBillBusinesRequest request, ModelStateDictionary modelState)
        {
            BillBusinessResponse response = new BillBusinessResponse();
            response.Errors = _billBusinessValidator.Validate(request, modelState).ToArray();

            if (request.Risk.Roles != null && request.Risk.Roles.Count() == 0)
                request.Risk.Roles = null;

            if (request.Risk.Address != null)
            {
                foreach (var address in request.Risk.Address) { if (String.IsNullOrEmpty(address.sRecType)) address.sRecType = null; }
            }

            request.Risk.DiscountsTaxesSurcharges = null;

            if (response.Errors.Count() == 0)
            {
                response = await _vtsConectorService.BillBusiness(request);
                response.IsSuccess = true;
            }
            else
            {
                if (response.Errors != null && response.Errors.Count() != 0) throw new ProductException(response);
            }

            return response;
        }

        public async Task<BillBusinessResponse> SetupBusiness(GetBillBusinesPhotosRequest request, ModelStateDictionary modelState)
        {
            BillBusinessResponse response = new BillBusinessResponse();

            PhotoValidationContainer photos = new PhotoValidationContainer
            {
                Photos = request.Photos?.Distinct().ToArray(),
                MinPhotos = _photosOptions.Value.MinPhotos,
                Validate = EsFotoRequerida(request)
            };

            response.Errors = photos.Validate ? _setUpBusinessValidator.Validate(request, modelState, photos).ToArray()
                : _setUpBusinessValidator.Validate(request, modelState).ToArray();
            
            if (response.Errors.Count() == 0)
            {
                var requestSetup = new GetBillBusinesRequest
                {
                    Risk = request.Risk,
                    UnderwrittingCaseId = request.UnderwrittingCaseId
                };
                response = await _vtsConectorService.SetupBusiness(requestSetup);
                response.IsSuccess = true;
            }
            else
            {
                if (response.Errors != null && response.Errors.Count() != 0) throw new ProductException(response);
            }

            if(photos.Photos != null && photos.Photos.Length > 0)
                await SavePictures(photos.Photos, response.UnderwrittingCaseId);

            return response;
        }

        public async Task SavePictures(string[] pictures, int caseId)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(_mongoDbOptions.Value.MongoDB));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            var mongoClient = new MongoClient(settings);

            IMongoDatabase db = mongoClient.GetDatabase("testdb");
            var cosmoDb = db.GetCollection<BsonDocument>("photos");

            var foto = new BsonDocument
            {
                {"caseId", caseId},
                {"photo", new BsonArray(pictures)}
            };
            try
            {
                await cosmoDb.InsertOneAsync(foto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetUnderWritingResponse> GetUnderWrittingStatus(UnderwritingCaseStatusCollection request, ModelStateDictionary modelState)
        {
            GetUnderWritingResponse response = new GetUnderWritingResponse();

            response.Errors = _getUnderWritingValidator.Validate(request, modelState).ToArray();

            if (response.Errors.Count() == 0)
            {
                response = await _vtsConectorService.GetUnderWrittingStatus(request);
                response.IsSuccess = true;
            }
            else
            {
                if (response.Errors != null && response.Errors.Count() != 0) throw new ProductException(response);
            }

            return response;
        }

        public async Task<SetupAndGetUnderWrittingResponse> SetupAndGetUnderWritting(GetBillBusinesRequest request, ModelStateDictionary modelState)
        {
            SetupAndGetUnderWrittingResponse response = new SetupAndGetUnderWrittingResponse();

            response.Errors = _setUpBusinessValidator.Validate(request, modelState).ToArray();

            if (response.Errors.Count() == 0)
            {
                response = await _vtsConectorService.SetupAndGetUnderWritting(request);
                response.IsSuccess = true;
            }
            else
            {
                if (response.Errors != null && response.Errors.Count() != 0) throw new ProductException(response);
            }

            return response;
        }

        public async Task<BaseResponse> SetupGetUnderAndPolicy(GetBillBusinesRequest request, ModelStateDictionary modelState)
        {
            BaseResponse response = new BaseResponse();

            response.Errors = _setUpBusinessValidator.Validate(request, modelState).ToArray();

            if (response.Errors.Count() == 0)
            {
                response = await _vtsConectorService.SetupGetUnderAndPolicy(request);

            }
            else
            {
                if (response.Errors != null && response.Errors.Count() != 0) throw new ProductException(response);
            }

            return response;
        }

        public async Task<BaseResponse> HealthCheck(ModelStateDictionary modelState)
        {
            BaseResponse healthCheck = new BaseResponse();

            try
            {
                healthCheck = await _vtsConectorService.HealthCheck();
            }
            catch (Exception ex)
            {
                healthCheck.Errors = new List<ErrorDS> { new ErrorDS { ID = 1, Key = ex.Message, Descr = ex.Message } }.ToArray();
                throw new ProductException(healthCheck);
            }

            return healthCheck;
        }

        private bool EsFotoRequerida(GetBillBusinesPhotosRequest request)
        {
            bool IdStruct = request.Risk.IdStruct == _photosOptions.Value.IdStruct;
            bool ProductCode = request.Risk.ProductCode == _photosOptions.Value.ProductCode;
            bool Ramo = request.Risk.LineOfBusiness == _photosOptions.Value.Ramo;

            bool requerida = IdStruct && ProductCode && Ramo;

            return requerida;
        }
    }
}

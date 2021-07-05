using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Product_Management_Core.DataValidation;
using Product_Management_Core.DTO;
using Product_Management_Core.DTO.GetEnviromentBusiness;
using Product_Management_Core.Services;
using Product_Management_Core.Services.Interfaces;
using System.Threading.Tasks;

namespace Product_Management.Controllers
{
    [Route("ProductManagement")]
    public class ProductManagementController : BaseController
    {
        private readonly IProductManagementService _productManagementService;
        private readonly ILogger<ProductManagementController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IProductManagementValidation _productManagementValidation;
        private readonly IXmlService _xmlService;

        public ProductManagementController(IProductManagementService productManagementService,
                                    ILogger<ProductManagementController> logger,
                                     IConfiguration configuration,
                                     IProductManagementValidation productManagementValidation,
                                     IXmlService xmlService) : base(logger)
        {
            _productManagementService = productManagementService;
            _logger = logger;
            _configuration = configuration;
            _productManagementValidation = productManagementValidation;
            _xmlService = xmlService;
        }

        [HttpPost]
        [Route("GetEnviromentBusiness")]
        public async Task<JsonResult> GetEnviromentBusiness([FromBody] GetEnviromentRequest request)
        {
            Log("Request", request);
            var response = await ExecuteActionToJsonAsync(() => _productManagementService.GetEnviromentBusines(request, ModelState));

            Log("Response", response);

            return Json(response);

        }

        [HttpPost]
        [Route("GetEnviromentBusinessById")]
        public async Task<JsonResult> GetEnviromentBusinessById([FromBody] GetEnviromentRequestById request)
        {
            Log("Request", request);

            var response = await ExecuteActionToJsonAsync(() => _productManagementService.GetEnviromentBusinesById(request, ModelState));

            Log("Response", response);

            return Json(response);
        }

        [HttpPost]
        [Route("BillBusiness")]
        public async Task<JsonResult> BillBusiness([FromBody] GetBillBusinesRequest request)
        {

            Log("Request", request);

            var response = await ExecuteActionToJsonAsync(() => _productManagementService.GetBillBusines(request, ModelState));

            Log("Response", response);

            return Json(response);
        }

        [HttpPost]
        [Route("SetupBusiness")]
        public async Task<JsonResult> SetupBusiness([FromBody] GetBillBusinesPhotosRequest request)
        {
            Log("Request", request);
            var response = await ExecuteActionToJsonAsync(() => _productManagementService.SetupBusiness(request, ModelState));
            
            Log("Request", request);

            return Json(response);
        }

        [HttpPost]
        [Route("GetUnderWritting")]
        public async Task<JsonResult> GetUnderWritting([FromBody] GaliciaConnectedServiceAdded.UnderwritingCaseStatusCollection request)
        {
            Log("Request", request);

            var response = await ExecuteActionToJsonAsync(() => _productManagementService.GetUnderWrittingStatus(request, ModelState));

            Log("Request", request);

            return Json(response);
        }

        [HttpPost]
        [Route("SetupAndGetUnderWritting")]
        public async Task<JsonResult> SetupAndGetUnderWritting([FromBody] GetBillBusinesRequest request)
        {
            Log("Request", request);

            var response = await ExecuteActionToJsonAsync(() => _productManagementService.SetupAndGetUnderWritting(request, ModelState));

            Log("Request", request);

            return Json(response);
        }


        [HttpPost]
        [Route("SetupGetUnderAndPolicy")]
        public async Task<JsonResult> SetupGetUnderAndPolicy([FromBody] GetBillBusinesRequest request)
        {
            Log("Request", request);

            var response = await ExecuteActionToJsonAsync(() => _productManagementService.SetupGetUnderAndPolicy(request, ModelState));

            Log("Request", request);

            return Json(response);
        }

        [HttpPost]
        [Route("HealthCheck")]
        public async Task<JsonResult> HealthCheck()
        {
            var response = await ExecuteActionToJsonAsync(() => _productManagementService.HealthCheck(ModelState));

            return Json(response);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Product_Management_Core.DTO;
using Product_Management_Core.Services.Interfaces;
using System.Threading.Tasks;

namespace SetupBusines.Controllers
{
    [Route("BillBusines")]
    public class BillBusinesController : Controller
    {

        private readonly IProductManagementService _productManagementService;
        private readonly ILogger<BillBusinesController> _logger;
        private readonly IConfiguration _configuration;

        public BillBusinesController(IProductManagementService productManagementService,
                                    ILogger<BillBusinesController> logger,
                                    IConfiguration configuration)
        {
            _productManagementService = productManagementService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("BillBusines")]
        public async Task<JsonResult> BillBusines([FromBody] GetBillBusinesRequest request)
        {
            var response = await _productManagementService.GetBillBusines(request);

            return Json(response);
        }

    }
}

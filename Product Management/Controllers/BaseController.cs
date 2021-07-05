using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product_Management_Core.Exceptions;
using Product_Management_Domain.Entities;
using Product_Management_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product_Management.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="productAction">Acción que ejecuta si supera las validaciones.</param>
        /// <param name="validation">Validación que lleva a cabo. Nulleable. Si el resultado es >0 dispara excepción.</param>
        /// <param name="errorDescriptionPostAction">Post-action que dispara una excepción si llega a ese punto con un !actionResult.IsSuccess</param>
        /// <returns></returns>
        protected async Task<ActionResult> ExecuteActionToJsonAsync<T>(Func<Task<T>> productAction,
                                                                       Func<IEnumerable<ErrorBase>> validation = null,
                                                                       string errorDescriptionPostAction = null) where T : BaseResponse
        {
            if (validation != null)
            {
                var validationResult = validation();
                Log("Validation result:", validationResult);
                if (validationResult.Count() > 0) throw new ValidationException(validationResult);
            }

            var actionResult = await productAction();
            Log("Action result:", actionResult);
            if (actionResult != null && actionResult.IsSuccess) return Json(actionResult);

            if (errorDescriptionPostAction != null)
            {
                Log("Errors post action result:", errorDescriptionPostAction);
                throw new ProductManagementException(errorDescriptionPostAction);
            }

            return null;
        }

        protected ActionResult ExecuteActionToJson<T>(Func<T> productAction, string errorDescriptionPostAction = null) where T : BaseResponse
        {
            var actionResult = productAction();
            Log("Action result:", actionResult);
            if (actionResult != null && actionResult.IsSuccess) return Json(actionResult);

            if (errorDescriptionPostAction != null)
            {
                Log("Errors post action result:", errorDescriptionPostAction);
                throw new Exception(errorDescriptionPostAction);
            }

            return null;
        }

        protected void Log(string info, object result)
        {
            _logger.LogDebug(info + " " + JsonConvert.SerializeObject(result));
        }
    }
}

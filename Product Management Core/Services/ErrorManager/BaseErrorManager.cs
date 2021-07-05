using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product_Management_Core.DataValidation.Attributes;
using Product_Management_Domain.Entities;
using Product_Management_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Product_Management_Core.Services.ErrorManager
{
    public abstract class BaseErrorManager<TEnum> where TEnum : Enum
    {

        protected readonly ErrorContainer _errorData;
        protected readonly ILogger _logger;

        public BaseErrorManager(ILogger logger)
        {
            _errorData = new ErrorContainer();
            _logger = logger;
        }

        public abstract ErrorDS GetErrorFromCatalog(TEnum error, params string[] extraData);
        public abstract ErrorDS GetErrorFromCatalog(TEnum error);
        public abstract ProductManagementError GetError(TEnum error);

        public IEnumerable<ErrorDS> GetErrorsFromModelState(ModelStateDictionary modelState)
        {
            var errors = modelState.Select(a => a.Value.Errors).ToList();
            var errorList = new List<ErrorDS>();
            foreach (var err in errors)
            {
                foreach (var e in err.ToList())
                {
                    // Deseraliza el error desde el model state con informacion como errorCode y fieldName
                    var attributeError = JsonConvert.DeserializeObject<ErrorFromAttribute>(e.ErrorMessage);

                    // Obtiene el error del catalogo y adjunta informacion del error
                    var errorFromCatalog = GetError((TEnum)(object)attributeError.ErrorCode);

                    errorList.Add(new ErrorDS
                    {
                        ID = attributeError.ErrorCode,
                        Descr = SetErrorDescription(attributeError, errorFromCatalog),
                        Key = attributeError.FieldName,
                        ParentKey = attributeError.ParentFieldName
                    });
                }
            }
            return errorList;
        }

        private string SetErrorDescription(ErrorFromAttribute errorFromAtribute, ProductManagementError originalError)
        {
            var descripcionFinal = string.Empty;
            switch (originalError.TipoError)
            {
                case TipoError.Required:
                    descripcionFinal = string.Format(originalError.Description, errorFromAtribute.CompleteFieldName);
                    break;
                case TipoError.LowerOrEqualThan:
                    descripcionFinal = string.Format(originalError.Description, errorFromAtribute.CompleteFieldName, "<= " + errorFromAtribute.MaxLength);
                    break;
                case TipoError.Exact:
                    descripcionFinal = string.Format(originalError.Description, errorFromAtribute.CompleteFieldName, errorFromAtribute.MaxLength);
                    break;
                case TipoError.HigherThan:
                    descripcionFinal = string.Format(originalError.Description, errorFromAtribute.CompleteFieldName, "> " + errorFromAtribute.MinLength);
                    break;
                case TipoError.HigherOrEqualThan:
                    descripcionFinal = string.Format(originalError.Description, errorFromAtribute.CompleteFieldName, ">= " + errorFromAtribute.MinLength);
                    break;
                case TipoError.Range:
                    descripcionFinal = string.Format(originalError.Description, errorFromAtribute.CompleteFieldName,
                        $"{errorFromAtribute.MinLength} <= x <= {errorFromAtribute.MaxLength}");
                    break;
                default:
                    break;
            }
            return descripcionFinal;
        }
    }
}

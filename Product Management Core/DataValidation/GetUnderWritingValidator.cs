using Microsoft.AspNetCore.Mvc.ModelBinding;
using Product_Management_Core.DataValidation.Interfaces;
using Product_Management_Core.DTO;
using Product_Management_Core.DTO.GetUnderWriting;
using Product_Management_Core.Services.ErrorManager;
using Product_Management_Domain.Entities;
using Product_Management_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Product_Management_Core.DataValidation
{
    public class GetUnderWritingValidator : BaseValidator<GetUnderWritingErrorCatalog>, IGetUnderWritingValidator
    {
        public GetUnderWritingValidator(IGetUnderWritingErrorManager errorManager)
            : base(errorManager as BaseErrorManager<GetUnderWritingErrorCatalog>)
        {
        }

        public IEnumerable<ErrorDS> Validate(GaliciaConnectedServiceAdded.UnderwritingCaseStatusCollection request, ModelStateDictionary modelState)
        {
            if (request is null)
            {
                _errorList.Add(new ErrorDS { ID = 0, Descr = "Error en el formato del request." });
                return _errorList;
            }

            AddErrorsFromModel(modelState);
            ValidateConditionalRequired(request);
            ValidateStaticValues(request);
            ValidateRulesAndFormats(request);

            return _errorList.OrderBy(a => a.ID);
        }

        private void AddErrorsFromModel(ModelStateDictionary modelState)
        {
            if (_errorList.Count == 0) _errorList.AddRange(_errorManager.GetErrorsFromModelState(modelState));
        }

        private void ValidateConditionalRequired(GaliciaConnectedServiceAdded.UnderwritingCaseStatusCollection request)
        {
        }

        private void ValidateStaticValues(GaliciaConnectedServiceAdded.UnderwritingCaseStatusCollection request)
        {
        }

        private void ValidateRulesAndFormats(GaliciaConnectedServiceAdded.UnderwritingCaseStatusCollection request)
        {
        }
    }
}

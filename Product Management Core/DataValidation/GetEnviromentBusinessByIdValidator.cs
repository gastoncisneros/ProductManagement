using Microsoft.AspNetCore.Mvc.ModelBinding;
using Product_Management_Core.DataValidation.Interfaces;
using Product_Management_Core.DTO.GetEnviromentBusiness;
using Product_Management_Core.Services.ErrorManager;
using Product_Management_Domain.Entities;
using Product_Management_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Product_Management_Core.DataValidation
{

    public class GetEnviromentBusinessByIdValidator : BaseValidator<GetEnviromentBusinessByIdErrorCatalog>, IGetEnviromentBusinessByIdValidator
    {
        public GetEnviromentBusinessByIdValidator(IGetEnviromentByIdErrorManager errorManager)
            : base(errorManager as BaseErrorManager<GetEnviromentBusinessByIdErrorCatalog>)
        {
        }

        public IEnumerable<ErrorDS> Validate(GetEnviromentRequestById request, ModelStateDictionary modelState)
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

        private void ValidateConditionalRequired(GetEnviromentRequestById request)
        {

        }

        private void ValidateStaticValues(GetEnviromentRequestById request)
        {

            ValidateProperty(propertyName: "commercialStructureId",
                conditionToError: () => request.commercialStructureId == 0,
                action: () => AddError(GetEnviromentBusinessByIdErrorCatalog.commercialStructureId, "commercialStructureId", "distinto de 0"));

        }

        private void ValidateRulesAndFormats(GetEnviromentRequestById request)
        {

        }
    }
}

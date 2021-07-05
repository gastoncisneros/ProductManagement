using Microsoft.AspNetCore.Mvc.ModelBinding;
using Product_Management_Core.DataValidation.Interfaces;
using Product_Management_Core.DTO;
using Product_Management_Core.Services.ErrorManager;
using Product_Management_Domain.Entities;
using Product_Management_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Product_Management_Core.DataValidation
{
    public class GetEnviromentBusinessValidator : BaseValidator<GetEnviromentBusinessErrorCatalog>, IGetEnviromentBusinessValidator
    {
        public GetEnviromentBusinessValidator(IGetEnviromentBusinessErrorManager errorManager)
            : base(errorManager as BaseErrorManager<GetEnviromentBusinessErrorCatalog>)
        {
        }

        public IEnumerable<ErrorDS> Validate(GetEnviromentRequest request, ModelStateDictionary modelState)
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

        private void ValidateConditionalRequired(GetEnviromentRequest request)
        {
            //ValidateProperty(propertyName: "firstComponentLevel",
            //    conditionToError: () => request.firstComponentLevel < 3,
            //    action: () => AddError(GetEnviromentBusinessErrorCatalog.firstId, "firstComponentLevel", "no puede ser menor que 3"));

        }

        private void ValidateStaticValues(GetEnviromentRequest request)
        {

            //ValidateProperty(propertyName: "firstComponentLevel",
            //    conditionToError: () => request.firstComponentLevel < 3,
            //    action: () => AddError(GetEnviromentBusinessErrorCatalog.firstComponentLevel, "firstComponentLevel", "3"));

            //ValidateProperty(propertyName: "secondComponentLevel",
            //    conditionToError: () => request.secondComponentLevel != 1,
            //    action: () => AddError(GetEnviromentBusinessErrorCatalog.secondComponentLevel, "secondComponentLevel", "1"));

            //ValidateProperty(propertyName: "thirdComponentLevel",
            //    conditionToError: () => request.thirdComponentLevel != 1,
            //    action: () => AddError(GetEnviromentBusinessErrorCatalog.thirdComponentLevel, "thirdComponentLevel", "1"));

            //var agreementValue = _optionsCache.FindValue<int>(CacheEntries.agreementCode);
            //ValidateProperty(propertyName: "agreementCode",
            //    conditionToError: () => request.riskData?.agreementCode != agreementValue,
            //    action: () => AddError(SetUpErrorCatalog.SV_agreementCode, "riskData.agreementCode", agreementValue.ToString()));

        }

        private void ValidateRulesAndFormats(GetEnviromentRequest request)
        {

            //ValidateProperty(propertyName: "uid",
            //    conditionToExecute: () => request.riskData?.uid != null,
            //    conditionToError: () => !ValidatorHelper.IsNumeric(request.riskData?.uid),
            //    action: () => AddError(SetUpErrorCatalog.RF_uid, "riskData.uid", "Debe ser numérico"));
        }
    }
}

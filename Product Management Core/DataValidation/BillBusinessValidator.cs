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
    public class BillBusinessValidator : BaseValidator<BillBusinessErrorCatalog>, IBillBusinessValidator
    {
        public BillBusinessValidator(IBillBusinessErrorManager errorManager)
            : base(errorManager as BaseErrorManager<BillBusinessErrorCatalog>)
        {
        }

        public IEnumerable<ErrorDS> Validate(GetBillBusinesRequest request, ModelStateDictionary modelState)
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

        private void ValidateConditionalRequired(GetBillBusinesRequest request)
        {
            foreach (var coverage in request.Risk.Coverages)
            {
                ValidateProperty(propertyName: "CoverageChangeIndicator",
                    conditionToError: () => coverage.CoverageChangeIndicator == null,
                    action: () => AddError(BillBusinessErrorCatalog.CoverageChangeIndicator, "CoverageChangeIndicator"));

                ValidateProperty(propertyName: "Selected",
                    conditionToError: () => coverage.Selected == null,
                    action: () => AddError(BillBusinessErrorCatalog.Selected, "Selected"));


                if (request.Risk.ParticularData.TypeOfParticularData == 2)
                {
                    ValidateProperty(propertyName: "HomeOwner",
                        conditionToError: () => request.Risk.ParticularData.HomeOwner == null,
                        action: () => AddError(BillBusinessErrorCatalog.HomeOwner, "HomeOwner"));
                }

                if (request.Risk.ParticularData.TypeOfParticularData == 1)
                {
                    ValidateProperty(propertyName: "Theft",
                        conditionToError: () => request.Risk.ParticularData.Theft == null,
                        action: () => AddError(BillBusinessErrorCatalog.HomeOwner, "Theft"));
                }
            }

        }

        private void ValidateStaticValues(GetBillBusinesRequest request)
        {

            ValidateProperty(propertyName: "UnderwrittingCaseId",
                conditionToError: () => request.UnderwrittingCaseId != 0,
                action: () => AddError(BillBusinessErrorCatalog.UnderwrittingCaseId, "UnderwrittingCaseId", "0"));

            ValidateProperty(propertyName: "TransactionId",
                conditionToError: () => request.Risk.ParticularData.TrasactionId != 1,
                action: () => AddError(BillBusinessErrorCatalog.TransactionId, "TransactionId", "1"));

            ValidateProperty(propertyName: "TypeOfParticularData",
                conditionToError: () => request.Risk.ParticularData.TypeOfParticularData != 1 && request.Risk.ParticularData.TypeOfParticularData != 2,
                action: () => AddError(BillBusinessErrorCatalog.TypeOfParticularData, "TypeOfParticularData", "1 o 2"));
        }

        private void ValidateRulesAndFormats(GetBillBusinesRequest request)
        {
            if (request.Risk.EndingDate != null && request.Risk.EndingDate != DateTime.MinValue)
            {
                ValidateProperty(propertyName: "EndingDate",
                    conditionToExecute: () => request.Risk.EndingDate != null && request.Risk.EffectiveDate != null,
                    conditionToError: () => (request.Risk.EndingDate - request.Risk.EffectiveDate).TotalHours < 24,
                    action: () => AddError(BillBusinessErrorCatalog.EndingDate, "EndingDate", "24 horas mayor a EffectiveDate"));
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Product_Management_Core.DataValidation.Attributes;
using Product_Management_Core.DataValidation.Interfaces;
using Product_Management_Core.DTO;
using Product_Management_Core.Helpers;
using Product_Management_Core.Services.ErrorManager;
using Product_Management_Domain.Entities;
using Product_Management_Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Product_Management_Core.DataValidation
{
    public class SetupBusinessValidator : BaseValidator<SetUpErrorCatalog>, ISetUpBusinessValidator
    {
        public SetupBusinessValidator(ISetupBusinessErrorManager errorManager)
            : base(errorManager as BaseErrorManager<SetUpErrorCatalog>)
        {
        }

        public IEnumerable<ErrorDS> Validate(GetBillBusinesRequest request, ModelStateDictionary modelState, PhotoValidationContainer photos = null)
        {
            if (request is null)
            {
                _errorList.Add(new ErrorDS { ID = 0, Descr = "Error en el formato del request." });
                return _errorList;
            }

            AddErrorsFromModel(modelState);
            ValidateConditionalRequired(request, photos);
            ValidateStaticValues(request, photos);
            ValidateRulesAndFormats(request, photos);

            return _errorList.OrderBy(a => a.ID);
        }

        private void AddErrorsFromModel(ModelStateDictionary modelState)
        {
            if (_errorList.Count == 0) _errorList.AddRange(_errorManager.GetErrorsFromModelState(modelState));
        }

        private void ValidateConditionalRequired(GetBillBusinesRequest request, PhotoValidationContainer photos = null)
        {
            foreach (var coverage in request.Risk.Coverages)
            {
                ValidateProperty(propertyName: "CoverageChangeIndicator",
                    conditionToError: () => coverage.CoverageChangeIndicator == null,
                    action: () => AddError(SetUpErrorCatalog.CoverageChangeIndicator, "CoverageChangeIndicator"));

                ValidateProperty(propertyName: "Selected",
                    conditionToError: () => coverage.Selected == null,
                    action: () => AddError(SetUpErrorCatalog.Selected, "Selected"));


                if (request.Risk.ParticularData.TypeOfParticularData == 2)
                {
                    ValidateProperty(propertyName: "HomeOwner",
                        conditionToError: () => request.Risk.ParticularData.HomeOwner == null,
                        action: () => AddError(SetUpErrorCatalog.HomeOwner, "HomeOwner"));
                }

                if (request.Risk.ParticularData.TypeOfParticularData == 1)
                {
                    ValidateProperty(propertyName: "Theft",
                        conditionToError: () => request.Risk.ParticularData.Theft == null,
                        action: () => AddError(SetUpErrorCatalog.HomeOwner, "Theft"));
                }

                if (photos != null)
                {

                    if (photos.Validate)
                    {
                        ValidateProperty(propertyName: "Photos",
                            conditionToError: () => photos.Photos == null,
                            action: () => AddError(SetUpErrorCatalog.PhotosRequired, "Photos"));

                        if (photos.Photos != null)
                        {
                            ValidateProperty(propertyName: "Photos",
                                conditionToError: () => photos.Photos.Length < photos.MinPhotos,
                                action: () => AddError(SetUpErrorCatalog.PhotosLength, "Photos", photos.MinPhotos.ToString()));
                        }
                    }
                }
            }
            foreach (var address in request.Risk.Address)
            {
                ValidateProperty(propertyName: "zipcode",
                    conditionToError: () => String.IsNullOrEmpty(address.ZipCode),
                    action: () => AddError(SetUpErrorCatalog.Required, "zipcode"));

                ValidateProperty(propertyName: "MunicipalityCode",
                    conditionToError: () => address.MunicipalityCode == 0,
                    action: () => AddError(SetUpErrorCatalog.Required, "MunicipalityCode"));

                ValidateProperty(propertyName: "Province",
                    conditionToError: () => address.Province == 0,
                    action: () => AddError(SetUpErrorCatalog.Required, "Province"));
            }

            foreach (var role in request.Risk.Roles)
            {
                ValidateProperty(propertyName: "zipcode",
                    conditionToError: () => String.IsNullOrEmpty(role.Address.ZipCode),
                    action: () => AddError(SetUpErrorCatalog.Required, "zipcode"));

                ValidateProperty(propertyName: "MunicipalityCode",
                    conditionToError: () => role.Address.MunicipalityCode == 0,
                    action: () => AddError(SetUpErrorCatalog.Required, "MunicipalityCode"));

                ValidateProperty(propertyName: "Province",
                    conditionToError: () => role.Address.Province == 0,
                    action: () => AddError(SetUpErrorCatalog.Required, "Province"));
            }
        }

        private void ValidateStaticValues(GetBillBusinesRequest request, PhotoValidationContainer photos = null)
        {

            ValidateProperty(propertyName: "UnderwrittingCaseId",
                conditionToError: () => request.UnderwrittingCaseId != 0,
                action: () => AddError(SetUpErrorCatalog.UnderwrittingCaseId, "UnderwrittingCaseId", "0"));

            ValidateProperty(propertyName: "TransactionId",
                conditionToError: () => request.Risk.ParticularData.TrasactionId != 1,
                action: () => AddError(SetUpErrorCatalog.TransactionId, "TransactionId", "1"));

            ValidateProperty(propertyName: "TypeOfParticularData",
                conditionToError: () => request.Risk.ParticularData.TypeOfParticularData != 1 && request.Risk.ParticularData.TypeOfParticularData != 2,
                action: () => AddError(SetUpErrorCatalog.TypeOfParticularData, "TypeOfParticularData", "1 o 2"));
        }

        private void ValidateRulesAndFormats(GetBillBusinesRequest request, PhotoValidationContainer photos = null)
        {
            ValidateProperty(propertyName: "BirthDate",
                conditionToExecute: () => request.Risk.Roles != null && request.Risk.Roles.Select(x => x.BirthDate) != null,
                conditionToError: () => !ValidationHelper.EsPersonaMayor(request.Risk.Roles.Select(x => x.BirthDate)),
                action: () => AddError(SetUpErrorCatalog.RF_BirthDate, "Roles.BirthDate", "Error de edad"));

            if (request.Risk.EndingDate != null && request.Risk.EndingDate != DateTime.MinValue)
            {
                ValidateProperty(propertyName: "EndingDate",
                    conditionToExecute: () => request.Risk.EndingDate != null && request.Risk.EffectiveDate != null,
                    conditionToError: () => (request.Risk.EndingDate - request.Risk.EffectiveDate).TotalHours < 24,
                    action: () => AddError(SetUpErrorCatalog.EndDate, "EndingDate", "24 horas mayor a EffectiveDate"));
            }
        }
    }
}

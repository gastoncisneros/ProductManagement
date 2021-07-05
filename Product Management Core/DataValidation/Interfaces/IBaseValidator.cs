using Microsoft.AspNetCore.Mvc.ModelBinding;
using Product_Management_Core.DataValidation.Attributes;
using Product_Management_Core.DTO;
using Product_Management_Core.DTO.GetEnviromentBusiness;
using Product_Management_Domain.Entities;
using System.Collections.Generic;

namespace Product_Management_Core.DataValidation.Interfaces
{
    public interface IProductManagementValidator<TRequest, TResponse> where TResponse : class
    {
        TResponse Validate(TRequest request, ModelStateDictionary modelState);
    }

    public interface IProductManagementValidatorSetup<TRequest, TResponse> where TResponse : class
    {
        TResponse Validate(TRequest request, ModelStateDictionary modelState, PhotoValidationContainer photos = null);
    }

    public interface ISetUpBusinessValidator : IProductManagementValidatorSetup<GetBillBusinesRequest, IEnumerable<ErrorDS>>
    {
    }

    public interface IBillBusinessValidator : IProductManagementValidator<GetBillBusinesRequest, IEnumerable<ErrorDS>>
    { }

    public interface IGetEnviromentBusinessValidator : IProductManagementValidator<GetEnviromentRequest, IEnumerable<ErrorDS>>
    {
    }

    public interface IGetEnviromentBusinessByIdValidator : IProductManagementValidator<GetEnviromentRequestById, IEnumerable<ErrorDS>>
    { }

    public interface IGetUnderWritingValidator : IProductManagementValidator<GaliciaConnectedServiceAdded.UnderwritingCaseStatusCollection, IEnumerable<ErrorDS>>
    {
    }
}

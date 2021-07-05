using Microsoft.AspNetCore.Mvc.ModelBinding;
using Product_Management_Core.DTO;
using Product_Management_Core.DTO.GetEnviromentBusiness;
using Product_Management_Core.DTO.GetUnderWriting;
using Product_Management_Core.DTO.SetupAndGetUnderWritting;
using Product_Management_Domain.Entities;
using System.Threading.Tasks;

namespace Product_Management_Core.Services.Interfaces
{
    public interface IProductManagementService
    {
        Task<GetEnviromentResponse> GetEnviromentBusines(GetEnviromentRequest request, ModelStateDictionary modelState);

        Task<GetEnviromentByIdResponse> GetEnviromentBusinesById(GetEnviromentRequestById request, ModelStateDictionary modelState);

        Task<BillBusinessResponse> GetBillBusines(GetBillBusinesRequest request, ModelStateDictionary modelState);

        Task<BillBusinessResponse> SetupBusiness(GetBillBusinesPhotosRequest request, ModelStateDictionary modelState);
        Task<GetUnderWritingResponse> GetUnderWrittingStatus(GaliciaConnectedServiceAdded.UnderwritingCaseStatusCollection request, ModelStateDictionary modelState);

        Task<SetupAndGetUnderWrittingResponse> SetupAndGetUnderWritting(GetBillBusinesRequest request, ModelStateDictionary modelState);

        Task<BaseResponse> SetupGetUnderAndPolicy(GetBillBusinesRequest request, ModelStateDictionary modelState);

        Task<BaseResponse> HealthCheck(ModelStateDictionary modelState);

    }
}

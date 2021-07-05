using Product_Management_Core.DTO;
using Product_Management_Core.DTO.GetEnviromentBusiness;
using Product_Management_Core.Exceptions;
using Product_Management_Core.Helpers;
using Product_Management_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Product_Management_Core.DataValidation
{
    public interface IProductManagementValidation
    {
        IEnumerable<ErrorBase> ValidateGetEnviromentBusiness(GetEnviromentRequest request);
        IEnumerable<ErrorBase> ValidateGetEnviromentBusinessById(GetEnviromentRequestById request);
        IEnumerable<ErrorBase> ValidateBillBusiness(GetBillBusinesRequest request);
    }

    public class ProductManagementValidation : IProductManagementValidation
    {
        private List<ErrorBase> _errorList { get; set; }

        public ProductManagementValidation()
        {
            _errorList = new List<ErrorBase>();
        }

        public IEnumerable<ErrorBase> ValidateGetEnviromentBusiness(GetEnviromentRequest request)
        {
            ValidateGetEnviroment(request);
            return _errorList;
        }
        public IEnumerable<ErrorBase> ValidateGetEnviromentBusinessById(GetEnviromentRequestById request)
        {
            ValidateGetEnviroment(request);
            return _errorList;
        }

        public IEnumerable<ErrorBase> ValidateBillBusiness(GetBillBusinesRequest request)
        {
            ValidateBillBusinessRequest(request);
            return _errorList;
        }

        public void ValidateGetEnviroment(GetEnviromentRequest request)
        {
            if (_errorList.Count > 0)
                return;

            if (!ValidationHelper.Required(request.firstComponentLevel)) _errorList.AddError("El campo firstComponentLevel no puede ser nulo");
            if (!ValidationHelper.Required(request.secondComponentLevel)) _errorList.AddError("El campo secondComponentLevel no puede ser nulo");
            if (!ValidationHelper.Required(request.thirdComponentLevel)) _errorList.AddError("El campo thirdComponentLevel no puede ser nulo");
        }

        public void ValidateGetEnviroment(GetEnviromentRequestById request)
        {
            if (_errorList.Count > 0)
                return;

        }

        public void ValidateBillBusinessRequest(GetBillBusinesRequest request)
        {
            if (_errorList.Count > 0)
                return;

            #region Coverage
            if (request.Risk.Coverages.Any(x => !ValidationHelper.Required(x.CoverageChangeIndicator))) _errorList.AddError("El campo CoverageChangeIndicator es requerido");
            if (request.Risk.Coverages.Any(x => !ValidationHelper.Required(x.Selected))) _errorList.AddError("El campo Selected es requerido");
            #endregion

            #region Particular Data
            //if (!request.Risk.ParticularData.TrasactionId.IsOne()) _errorList.AddError("El campo TrasactionId tiene que ser 1");

            if (!ValidationHelper.TPDIsValid(request.Risk.ParticularData.TypeOfParticularData)) _errorList.AddError("El campo TypeOfParticularData debe ser 1 o 2");
            #endregion


        }

    }
}

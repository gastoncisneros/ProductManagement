using Microsoft.Extensions.Logging;
using Product_Management_Domain.Entities;
using Product_Management_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Product_Management_Core.Services.ErrorManager
{
    public interface ISetupBusinessErrorManager
    {
    }

    public class SetupBusinessErrorManager : BaseErrorManager<SetUpErrorCatalog>, ISetupBusinessErrorManager
    {
        public SetupBusinessErrorManager(ILogger<SetupBusinessErrorManager> logger) : base(logger)
        {
        }

        public override ProductManagementError GetError(SetUpErrorCatalog error)
        {
            var result = _errorData.SetUpBusinessErrorList.Where(a => a.Id == (int)error).FirstOrDefault();
            if (result is null)
            {
                _logger.LogDebug($"Error id not found: {(int)error}, {error.ToString()}");
                result = new ProductManagementError();
            }

            return result;
        }

        public override ErrorDS GetErrorFromCatalog(SetUpErrorCatalog error, params string[] extraData)
        {
            var errorCatalog = GetError(error);
            var description = extraData != null && extraData.Count() > 0 ? string.Format(errorCatalog.Description, extraData) : errorCatalog.Description;

            var errorKey = ((SetUpErrorCatalog)errorCatalog.Id).ToString();
            var splitedErrorKey = errorKey.Split('_');
            var errorNamePosition = splitedErrorKey.Count() - 1;

            return new ErrorDS { ID = errorCatalog.Id, Descr = description, Key = splitedErrorKey[errorNamePosition] };
        }

        public override ErrorDS GetErrorFromCatalog(SetUpErrorCatalog error)
        {
            var errorCatalog = GetError(error);

            var errorKey = ((SetUpErrorCatalog)errorCatalog.Id).ToString();
            var errorName = errorKey.Split('_');
            var number = errorName.Count() - 1;

            return new ErrorDS { ID = errorCatalog.Id, Descr = errorCatalog.Description, Key = errorName[number] };
        }
    }
}

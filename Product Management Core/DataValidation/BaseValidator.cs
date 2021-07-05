using Product_Management_Core.Services.ErrorManager;
using Product_Management_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Product_Management_Core.DataValidation
{
    public abstract class BaseValidator<TEnum> where TEnum : Enum
    {
        protected readonly BaseErrorManager<TEnum> _errorManager;
        protected List<ErrorDS> _errorList { get; set; }

        public BaseValidator(BaseErrorManager<TEnum> errorManager)
        {
            _errorList = new List<ErrorDS>();
            _errorManager = errorManager;
        }
        protected bool GetPropertyCanValidate(string propertyName, string parentPropertyName = null)
        {
            var canValidate = true;

            if (parentPropertyName != null)
                canValidate &= !_errorList.Any(a => a.ParentKey == parentPropertyName && a.Key == propertyName);
            else
                canValidate &= !_errorList.Any(a => a.Key == propertyName);

            return canValidate;
        }

        protected void ValidateProperty(string propertyName, Func<bool> conditionToError, Action action, Func<bool> conditionToExecute = null, string parentPropertyName = null)
        {
            var conditionToExc = conditionToExecute ?? (() => true);

            if (GetPropertyCanValidate(propertyName, parentPropertyName))
                if (conditionToExc())
                    if (conditionToError())
                        action();
        }

        protected ErrorDS GetError(TEnum error, params string[] extraData)
        {
            return _errorManager.GetErrorFromCatalog(error, extraData);
        }

        protected void AddError(TEnum error, params string[] extraData)
        {
            _errorList.Add(GetError(error, extraData));
        }
    }
}

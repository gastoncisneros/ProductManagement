using Product_Management_Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.Exceptions
{
    public static class ErrorExtension
    {
        public static void AddError(this List<ErrorBase> errorList, string errorDescription)
        {
            if (errorList != null)
                errorList.Add(new ErrorBase { Descripcion = errorDescription });
        }
    }
}

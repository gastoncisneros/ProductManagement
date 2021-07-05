using Newtonsoft.Json;
using Product_Management_Domain.Entities;
using Product_Management_Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.Exceptions
{
    public class ProductManagementException : Exception
    {
        public ProductManagementException(object data) : base(JsonConvert.SerializeObject(data))
        {
            DataObject = data;
        }

        public object DataObject { get; private set; }
    }

    public class ValidationException : ProductManagementException
    {
        public ValidationException(IEnumerable<ErrorBase> errors) : base(errors)
        {

        }
    }

    public class ProductException : ProductManagementException
    {
        public ProductException(BaseResponse response) : base(response)
        {

        }
    }
}

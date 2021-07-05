using Product_Management_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.DTO.OnDemand
{
    public class OnDemandResponse : BaseResponse
    {
        public OnDemandResponse()
        {
            IsSuccess = true;
        }

        public string Base64 { get; set; }
    }

    public class ErrorResponse
    {
        public int ID { get; set; }
        public string Descr { get; set; }
    }

    public class OnDemandError : BaseResponse
    {
        public OnDemandError()
        {
        }
        public ErrorResponse Err { get; set; }
    }
}

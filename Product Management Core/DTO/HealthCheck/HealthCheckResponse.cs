using Product_Management_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.DTO.HealthCheck
{
    public class HealthCheckResponse : BaseResponse
    {
        public HealthCheckResponse()
        {
            IsSuccess = true;
        }
        public string Respuesta_Manager { get; set; }
        public string Respuesta_Argentina { get; set; }
    }

    public class ErrorResponse
    {
        public int ID { get; set; }
        public string Descr { get; set; }
    }

    public class HealthCheckError : BaseResponse
    {
        public HealthCheckError()
        {
        }
        public ErrorResponse Err { get; set; }
    }
}

using Product_Management_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.DTO.GetEnviromentBusiness
{
    public class GetEnviromentResponse : BaseResponse
    {
        public Response[] Response { get; set; }
    }

    public class GetEnviromentByIdResponse : BaseResponse
    {
        public Response Response { get; set; }
    }

    public class Response
    {
        public IEnumerable<GaliciaSegurosReference.StrucComponent> Components { get; set; }
        public string Message { get; set; }
        public IEnumerable<GaliciaSegurosReference.StrucProducts> Products { get; set; }
        public int StructureCode { get; set; }
        public string StructureDescription { get; set; }
    }
}

using GaliciaConnectedServiceAdded;
using Product_Management_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.DTO.GetUnderWriting
{
    public class GetUnderWritingResponse : BaseResponse
    {
        public List<UnderwritingCaseStatusResult> Response { get; set; }
    }
}

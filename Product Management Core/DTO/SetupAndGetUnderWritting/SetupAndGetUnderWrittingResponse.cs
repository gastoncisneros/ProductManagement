using Product_Management_Core.DTO.GetUnderWriting;
using Product_Management_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.DTO.SetupAndGetUnderWritting
{
    public class SetupAndGetUnderWrittingResponse : BaseResponse
    {
        public BillBusinessResponse SetupResponse { get; set; }
        public GetUnderWritingResponse UnderWrittingResponse { get; set; }
    }

    public class SetupAndGetUnderAndPolicyResponse : BaseResponse
    {
        public BillBusinessResponse SetupResponse { get; set; }
        public GetUnderWritingResponse UnderWrittingResponse { get; set; }
        public string Base64 { get; set; }

    }
}

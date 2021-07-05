using GaliciaSegurosReference;
using Product_Management_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.DTO
{
    public class GetBillBusinesRequest
    {
        public Risk Risk { get; set; }
        public int UnderwrittingCaseId { get; set; }
    }

}

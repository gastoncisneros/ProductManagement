using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.DTO
{
    public class GetEnviromentRequest
    {
        public DateTime effectiveDate { get; set; }
        public int firstComponentLevel { get; set; }
        public int secondComponentLevel { get; set; }
        public int thirdComponentLevel { get; set; }
    }

}

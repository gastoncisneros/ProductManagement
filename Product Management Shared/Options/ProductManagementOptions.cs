using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Shared.Options
{
    public class ProductManagementOptions
    {
        public string WebServiceUrl { get; set; }
        public int DelayFunction { get; set; }
        public OnDemandOptions OnDemandOptions { get; set; }
    }
}

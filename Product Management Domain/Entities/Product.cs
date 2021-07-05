using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Domain.Entities
{
    public class BaseResponse
    {
        [JsonIgnore]
        public bool IsSuccess { get; set; }
        public ErrorDS[] Errors { get; set; }
    }

    public class ProductClientResponse : BaseResponse
    {
        public IEnumerable<Component_Local> Components { get; set; }
        public string Message { get; set; }
        public IEnumerable<Product_Local> Products { get; set; }
        public int StructureCode { get; set; }
        public string StructureDescription { get; set; }
    }

    public class ErrorDS
    {
        public int ID { get; set; }
        public string Descr { get; set; }

        [JsonIgnore]
        public string Key { get; set; }

        [JsonIgnore]
        public string ParentKey { get; set; }

    }

}

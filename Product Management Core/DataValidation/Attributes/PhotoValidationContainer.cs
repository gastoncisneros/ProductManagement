using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.DataValidation.Attributes
{
    public class PhotoValidationContainer
    {
        public string[] Photos { get; set; }
        public bool Validate { get; set; }
        public int MinPhotos { get; set; }
    }
}

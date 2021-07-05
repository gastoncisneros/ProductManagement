using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.DataValidation.Attributes
{
    /// <summary>
    /// Agrupa los errores de validación encontrados en los atributos del modelo.
    /// Linkea con Error (ProductManagementError) con ErrorCode
    /// </summary>
    public class ErrorFromAttribute
    {
        public int ErrorCode { get; set; }
        public string ParentFieldName { get; set; }
        public string FieldName { get; set; }
        public string CompleteFieldName
        {
            get => $"{ParentFieldName}.{FieldName}";
        }
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.DTO.SetupUnderWrittingAndPolicyDoc
{
    public class GetPolicyDocRequest
    {
        public long? PolicyId { get; set; }
        public int? Branch { get; set; }
        public int? Product { get; set; }
        public long? CertificateID { get; set; }
        public DateTime? Date { get; set; }
        public string DocNumber { get; set; }
        public string Sexo { get; set; }
        public string TipoDoc { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Origen { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Core.DTO.OnDemand
{
    public class OnDemandRequest
    {
        public string method { get; set; }
        public string credentials { get; set; }
        public string sCertype { get; set; }
        public string nBranch { get; set; }
        public string nProduct { get; set; }
        public string nPolicy { get; set; }
        public string nCertif { get; set; }
        public string dStArtDate { get; set; }
        public string modulo { get; set; }
        public string polizaProd { get; set; }
        public string cLote { get; set; }
        public string origen { get; set; }
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string tipoDoc { get; set; }
        public string nroDoc { get; set; }

        private string _sexo;
        public string sexo
        {
            get => _sexo;
            set
            {
                _sexo = value == "F" ? "1" : "2";
            }
        }
        public string email { get; set; }
        public string nOriginCode { get; set; }
        public string nLetTernum { get; set; }
        public string ts_user_id { get; set; }
    }
}

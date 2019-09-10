using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISAI_APP.Models.DTOS
{
    public class ObjOcr
    {
        public string[] TextoCompleto { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }
        public string Curp { get; set; }
        public string Domicilio { get; set; }
    }
}
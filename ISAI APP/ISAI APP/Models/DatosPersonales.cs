namespace ISAI_APP.Models
{
    using System;
    using System.Collections.Generic;

    public partial class DatosPersonales
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FechaNacimiento { get; set; }
        public string CURP { get; set; }
    }
}

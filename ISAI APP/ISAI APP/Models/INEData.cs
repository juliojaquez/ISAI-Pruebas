using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISAI_APP.Models
{
    [Table("INE")]
    public partial class INEData
    {
        public int id { get; set; }
        [StringLength(20)]
        public string name { get; set; }
        [StringLength(25)]
        public string firstName { get; set; }
        [StringLength(25)]
        public string lastName { get; set; }
        [StringLength(50)]
        public string home { get; set; }
        public int age { get; set; }
        [StringLength(1)]
        public string sex { get; set; }
        public int code { get; set; }
        [StringLength(20)]
        public string stateRepublic { get; set; }
        public int folio { get; set; }
        public int registerYear { get; set; }
        [StringLength(25)]
        public string codeElector { get; set; }
        [StringLength(25)]
        public string CURP { get; set; }
        public int stateNumber { get; set; }
        public int municipalityNumber { get; set; }
        public int locationNumber { get; set; }
        public int sectionNumber { get; set; }
        public int validUntil { get; set; }
        public string textComplete { get; set; }
        public string[] textCompleto { get; set; }
        public DateTime registerDate { get; set; }
    }
}
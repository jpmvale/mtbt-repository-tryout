using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MchRepositoryTryout.Models
{
    [Table("tbtraincompo")]
    public class Composition
    {
        [Key, Column("compokey", Order = 0)]
        public int CompoKey { get; set; }
        [Key, Column("tipo", Order = 1)]
        public string Type { get; set; }
        [Key, Column("pmt_id", Order = 2)]
        public string PmtID { get; set; }
        [Column("os")]
        public string OS { get; set; }
        [Column("peso_ind")]
        public double IndividualWeight { get; set; }
        [Column("serie")]
        public string Series { get; set; }
        [Column("pos")]
        public int Position { get; set; }
    }
}
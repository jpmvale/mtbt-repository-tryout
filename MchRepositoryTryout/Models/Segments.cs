using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MchRepositoryTryout.Models
{
    [Table("tbsegment")]
    public class Segments 
    {
        [Key, Column("location", Order = 0)]
        public int Location { get; set; }
        [Column("segment", Order = 1)]
        public string Segment { get; set; }
        //public int Trains { get; set; }
    }
}
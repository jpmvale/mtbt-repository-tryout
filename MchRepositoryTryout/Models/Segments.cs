using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MchRepositoryTryout.Models
{
    [Table("tbsegments")]
    public class Segments 
    {
        [Column("location")]
        public int Location { get; set; }
        [Column("segment")]
        public string Segment { get; set; }
    }
}
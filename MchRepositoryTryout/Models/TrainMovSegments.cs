using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MchRepositoryTryout.Models
{
    [Table("tbtrainmovsegment")]
    public class TrainMovSegments
    {
        [Key, Column("train_id", Order = 0)]
        public long TrainID { get; set; }
        [Key, Column("data_ocup", Order = 1)]
        public DateTime OcupationDate { get; set; }
        [Key, Column("location", Order = 2)]
        public int Location { get; set; }
        [Key, Column("ud", Order = 3)]
        public string Segment { get; set; }
        [Column("track")]
        public int Track { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MchRepositoryTryout.Models
{
    [Table("tbtrain")]
    public class Train
    {
        [Column("train_id")]
        public long TrainID { get; set; }
        [Column("name")]
        public string TrainName { get; set; }
        [Column("OSSGF")]
        public string OS { get; set; }
    }
}
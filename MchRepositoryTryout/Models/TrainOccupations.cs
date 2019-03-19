using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MchRepositoryTryout.Models
{
    public class TrainOccupations
    {
        public int[] TrackAndOccupation { get; set; } = new int[2];
        public long TrainID { get; set; }

    }
}
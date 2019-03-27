using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MchRepositoryTryout.Models
{
    public class Mch
    {
        public int Id { get; set; }
        public string InstallLocation { get; set; }
        public int LoadedTrains { get; set; }
        public int EmptyTrains { get; set; }
        public double MTBT { get; set; }
    }
}
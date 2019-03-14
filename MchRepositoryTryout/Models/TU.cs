using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MchRepositoryTryout.Models
{
    public class TU
    {
        public int Km { get; set; }
        public List<Mch> MchsInTu { get; set; } = new List<Mch>();
    }
}
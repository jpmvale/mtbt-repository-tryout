using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MchRepositoryTryout.Models
{
    public class Amv
    {
        public int AmvNumber { get; set; }
        public List<Mch> MchsInAmv { get; set; } = new List<Mch>();
    }
}
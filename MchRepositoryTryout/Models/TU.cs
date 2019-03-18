using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MchRepositoryTryout.Models
{

    public class TU
    {
        public enum TypesOfTU
        {
            A = 0,
            V = 1,
        };

        public int Km { get; set; }
        public TypesOfTU TypeOfTU { get; set; }
        public List<Amv> AmvsInTU { get; set; } = new List<Amv>();
    }
}
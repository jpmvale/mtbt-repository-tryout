using MchRepositoryTryout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MchRepositoryTryout.Services
{
    public class MchService
    {
        public static List<Mch> GetMchs()
        {
            var mchs = new List<Mch>(){
                new Mch {Id = 1, InstallLocation = "W11AK007" , MTBT = 500 ,Trains = 56},
                new Mch {Id = 2, InstallLocation = "W12AK007", MTBT = 750, Trains = 73 },
                new Mch {Id = 3, InstallLocation = "W21AK007", MTBT = 1300, Trains = 215 }
            };
            return mchs;
        }
    }
}
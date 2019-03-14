using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MchRepositoryTryout.Models;

namespace MchRepositoryTryout.Services
{
    public class TuService
    {
        public static List<TU> GetTu()
        {

            var tu = new List<TU>(){
                new TU {Km = 7, MchsInTu = new List<Mch>(){
                    new Mch {Id = 1, InstallLocation = "W11AK007" , MTBT = 500 ,Trains = 56},
                    new Mch {Id = 2, InstallLocation = "W12AK007", MTBT = 750, Trains = 73 },
                    new Mch {Id = 3, InstallLocation = "W21AK007", MTBT = 1300, Trains = 215 }
                    }
                },
                new TU {Km = 13, MchsInTu = new List<Mch>(){
                    new Mch {Id = 4, InstallLocation = "W11AK013" , MTBT = 600 ,Trains = 60},
                    new Mch {Id = 5, InstallLocation = "W12AK013", MTBT = 340, Trains = 32 },
                    new Mch {Id = 6, InstallLocation = "W21AK013", MTBT = 900, Trains = 89 }
                    }
                }
            };
            return tu;
        }
    }
}
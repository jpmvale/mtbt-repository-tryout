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
                new TU {Km = 7, AmvsInTU = new List<Amv>(){
                    new Amv{AmvNumber = 1, MchsInAmv = new List<Mch>(){ //AMV1
                        new Mch {Id = 1, InstallLocation = "W11A" , MTBT = 500 ,Trains = 56},
                        new Mch {Id = 2, InstallLocation = "W11B", MTBT = 750, Trains = 73 },
                        new Mch {Id = 3, InstallLocation = "W11C", MTBT = 1300, Trains = 140 }
                        }
                    },
                    new Amv{AmvNumber = 2, MchsInAmv = new List<Mch>(){ //AMV2
                        new Mch{Id = 4, InstallLocation = "W21A", MTBT = 734, Trains = 71},
                        new Mch{Id = 5, InstallLocation = "W21B", MTBT = 1200, Trains = 130},
                        new Mch{Id = 6, InstallLocation = "W21C", MTBT = 450, Trains = 53},
                        }
                    },
                    new Amv{AmvNumber = 3, MchsInAmv = new List<Mch>(){ //AMV3
                        new Mch{Id = 7, InstallLocation = "W12A", MTBT = 642, Trains = 64},
                        new Mch{Id = 8, InstallLocation = "W12B", MTBT = 878, Trains = 79},
                        new Mch{Id = 9, InstallLocation = "W12C", MTBT = 746, Trains = 74},
                        }
                    },
                    new Amv{AmvNumber = 4, MchsInAmv = new List<Mch>(){ //AMV4
                        new Mch{Id = 10, InstallLocation = "W22A", MTBT = 985, Trains = 97},
                        new Mch{Id = 11, InstallLocation = "W22B", MTBT = 648, Trains = 65},
                        new Mch{Id = 12, InstallLocation = "W22C", MTBT = 971, Trains = 90},
                        }
                    }
                    }
                },
                new TU {Km = 13, AmvsInTU = new List<Amv>(){
                    new Amv{AmvNumber = 1,  MchsInAmv = new List<Mch>(){ //AMV1
                        new Mch {Id = 13, InstallLocation = "W11A" , MTBT = 540 ,Trains = 57},
                        new Mch {Id = 14, InstallLocation = "W11B", MTBT = 723, Trains = 69 },
                        new Mch {Id = 15, InstallLocation = "W11C", MTBT = 1256, Trains = 130 }
                        }
                    },
                    new Amv{AmvNumber = 2, MchsInAmv = new List<Mch>(){ //AMV2
                        new Mch{Id = 16, InstallLocation = "W21A", MTBT = 764, Trains = 71},
                        new Mch{Id = 17, InstallLocation = "W21B", MTBT = 1400, Trains = 145},
                        new Mch{Id = 18, InstallLocation = "W21C", MTBT = 450, Trains = 47},
                        }
                    },
                    new Amv{AmvNumber = 3, MchsInAmv = new List<Mch>(){ //AMV3
                        new Mch{Id = 19, InstallLocation = "W12A", MTBT = 638, Trains = 62},
                        new Mch{Id = 20, InstallLocation = "W12B", MTBT = 886, Trains = 81},
                        new Mch{Id = 21, InstallLocation = "W12C", MTBT = 723, Trains = 69},
                        }
                    },
                    new Amv{AmvNumber = 4, MchsInAmv = new List<Mch>(){ //AMV4
                        new Mch{Id = 22, InstallLocation = "W22A", MTBT = 1003, Trains = 102},
                        new Mch{Id = 23, InstallLocation = "W22B", MTBT = 632, Trains = 59},
                        new Mch{Id = 24, InstallLocation = "W22C", MTBT = 889, Trains = 85},
                        }
                    }
                    }
                }
            };
            return tu;
        }
    }
}
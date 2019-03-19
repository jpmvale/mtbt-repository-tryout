using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MchRepositoryTryout.DAL;
using MchRepositoryTryout.Models;

namespace MchRepositoryTryout.Services
{
    public class TuService
    {
        public static List<TU> GetTu()
        {
            var sgfContext = new SgfContext();
            var segments = sgfContext.Segments.Where(x => x.Segment == "WT").OrderBy(x => x.Location).ToList();
            var tu = new List<TU>();
            int auxId = new int();
            auxId = 1;

            foreach (var segment in segments.Take(10))
            {
                var trainBySegmentObjects = GetTrainsBySegment(segment.Location);
                int trainBySegment = trainBySegmentObjects.Count;
                var trains = trainBySegmentObjects.Select(x => x.TrainID).Distinct();
                Dictionary<string, int> keyValuePairs = new Dictionary<string, int>()
                {
                    ["W11A"] = 0,
                    ["W11B"] = 0,
                    ["W11C"] = 0,
                    ["W21A"] = 0,
                    ["W21B"] = 0,
                    ["W21C"] = 0,
                    ["W12A"] = 0,
                    ["W12B"] = 0,
                    ["W12C"] = 0,
                    ["W22A"] = 0,
                    ["W22B"] = 0,
                    ["W22C"] = 0,

                };

                foreach (var train in trains)
                {
                    var lm = trainBySegmentObjects.Where(x => x.TrainID == train).OrderBy(x => x.OcupationDate);
                    var tracks = lm.Select(x => x.Track).ToList();
                    var mchs = GetMchByOccupations(tracks);
                    mchs.ForEach(x => keyValuePairs[x]++);
                }

                tu.Add(
                  new TU
                  {
                      Km = segment.Location,
                      AmvsInTU = new List<Amv>()
                      {
                        new Amv{AmvNumber = 1, MchsInAmv = new List<Mch>(){ //AMV1
                            new Mch {Id = auxId++, InstallLocation = "W11A" , MTBT = 1.3*trainBySegment/2 ,Trains = trainBySegment/2},
                            new Mch {Id = auxId++, InstallLocation = "W11B", MTBT = 1.3*trainBySegment/2 , Trains = trainBySegment/2 },
                            new Mch {Id = auxId++, InstallLocation = "W11C", MTBT = 1.3*trainBySegment/2 , Trains = trainBySegment/2 }
                            }
                        },
                        new Amv{AmvNumber = 2, MchsInAmv = new List<Mch>(){ //AMV2
                            new Mch{Id = auxId++, InstallLocation = "W21A", MTBT = 1.3*trainBySegment/2, Trains = trainBySegment/2},
                            new Mch{Id = auxId++, InstallLocation = "W21B", MTBT = 1.3*trainBySegment/2, Trains = trainBySegment/2},
                            new Mch{Id = auxId++, InstallLocation = "W21C", MTBT = 1.3*trainBySegment/2, Trains = trainBySegment/2},
                            }
                        },
                        new Amv{AmvNumber = 3, MchsInAmv = new List<Mch>(){ //AMV3
                            new Mch{Id = auxId++, InstallLocation = "W12A", MTBT = 1.3*trainBySegment/2, Trains = trainBySegment/2},
                            new Mch{Id = auxId++, InstallLocation = "W12B", MTBT = 1.3*trainBySegment/2, Trains = trainBySegment/2},
                            new Mch{Id = auxId++, InstallLocation = "W12C", MTBT = 1.3*trainBySegment/2, Trains = trainBySegment/2},
                            }
                        },
                        new Amv{AmvNumber = 4, MchsInAmv = new List<Mch>(){ //AMV4
                            new Mch{Id = auxId++, InstallLocation = "W22A", MTBT = 1.3*trainBySegment/2, Trains = trainBySegment/2},
                            new Mch{Id = auxId++, InstallLocation = "W22B", MTBT = 1.3*trainBySegment/2, Trains = trainBySegment/2},
                            new Mch{Id = auxId++, InstallLocation = "W22C", MTBT = 1.3*trainBySegment/2, Trains = trainBySegment/2},
                            }
                        }
                      }
                  });
                trainBySegment = 0;
            }
            sgfContext.Dispose();

            List<TrainMovSegments> GetTrainsBySegment(int location)
            {
                return sgfContext.TrainMovSegments.Where(x => x.Segment == "WT" &&
                       x.Location == location && x.OcupationDate.Year == 2019 &&
                       x.OcupationDate.Month == 3).ToList();
            }

            List<string> GetMchByOccupations(List<int> tracks)
            {
                var amv_1 = new List<string>() { "W11A", "W11B", "W11C" };
                var amv_2 = new List<string>() { "W21C", "W21B", "W21A" };
                var amv_3 = new List<string>() { "W12C", "W12B", "W12A" };
                var amv_4 = new List<string>() { "W22A", "W22B", "W22C" };
                var myList = new List<string>();

                if (tracks.Count == 1 || tracks.Distinct().Count() == 1)
                {
                    if (tracks[0] == 1)
                        myList = myList.Concat(amv_1).Concat(amv_2).ToList();
                    else if (tracks[0] == 2)
                        myList = myList.Concat(amv_3).Concat(amv_4).ToList();
                }
                else if (tracks.Count == 2)
                {
                    if (tracks[0] == 1 && tracks[1] == 2)
                        myList = myList.Concat(amv_2).Concat(amv_3).Concat(amv_4).ToList();
                    else if (tracks[0] == 2 && tracks[1] == 1)
                        myList = myList.Concat(amv_1).Concat(amv_3).Concat(amv_4).ToList();
                }
                return myList;
            }

            sgfContext.Dispose();
            return tu;
        }
    }
}
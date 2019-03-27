using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MchRepositoryTryout.DAL;
using MchRepositoryTryout.Models;

namespace MchRepositoryTryout.Services
{
    public class TuService
    {
        private static List<int> typeA = new List<int>() { 396, 431, 442, 450 };
        public static List<TU> GetTu(DateTime initialDate, DateTime finalDate, int initialKM = 0, int finalKM = 892)
        {
            var sgfContext = new SgfContext();
            var segments = sgfContext.Segments.Where(x => x.Segment == "WT" && x.Location >= initialKM &&
                           x.Location <= finalKM).OrderBy(x => x.Location).ToList();
            var tu = new List<TU>();
            int auxId = new int();
            auxId = 1;

            foreach (var segment in segments)
            {
                var trainBySegmentObjects = GetTrainsBySegment(sgfContext, segment.Location, initialDate, finalDate);
                int trainBySegment = trainBySegmentObjects.Count;
                var trains = trainBySegmentObjects.Select(x => x.TrainID).Distinct();
                Dictionary<string, Tuple<int, int>> keyValuePairs = new Dictionary<string, Tuple<int, int>>()
                {
                    ["W11A"] = new Tuple<int, int>(0, 0),
                    ["W11B"] = new Tuple<int, int>(0, 0),
                    ["W11C"] = new Tuple<int, int>(0, 0),
                    ["W21A"] = new Tuple<int, int>(0, 0),
                    ["W21B"] = new Tuple<int, int>(0, 0),
                    ["W21C"] = new Tuple<int, int>(0, 0),
                    ["W12A"] = new Tuple<int, int>(0, 0),
                    ["W12B"] = new Tuple<int, int>(0, 0),
                    ["W12C"] = new Tuple<int, int>(0, 0),
                    ["W22A"] = new Tuple<int, int>(0, 0),
                    ["W22B"] = new Tuple<int, int>(0, 0),
                    ["W22C"] = new Tuple<int, int>(0, 0),

                }; // Item1 = Empty, Item2 = Loaded

                foreach (var train in trains)
                {
                    var lm = trainBySegmentObjects.Where(x => x.TrainID == train).OrderBy(x => x.OcupationDate);
                    var direction = lm.FirstOrDefault().Direction;
                    var tracks = lm.Select(x => x.Track).ToList();
                    TU.TypesOfTU type = typeA.Contains(segment.Location) ? TU.TypesOfTU.A : TU.TypesOfTU.V;
                    var mchs = GetMchByOccupations(tracks, direction, type);

                    if (direction == 1) // Empty Wagon
                        mchs.ForEach(x => keyValuePairs[x] = new Tuple<int, int>(keyValuePairs[x].Item1 + 1, keyValuePairs[x].Item2));
                    else // Loaded Wagon
                        mchs.ForEach(x => keyValuePairs[x] = new Tuple<int, int>(keyValuePairs[x].Item1, keyValuePairs[x].Item2 + 1));
                }

                tu.Add(
                  new TU
                  {
                      Km = segment.Location,
                      TypeOfTU = typeA.Contains(segment.Location) ? TU.TypesOfTU.A : TU.TypesOfTU.V,
                      AmvsInTU = new List<Amv>()
                      {
                        new Amv{AmvNumber = 1, MchsInAmv = new List<Mch>(){ //AMV1
                            new Mch {Id = auxId++, InstallLocation = "W11A", MTBT = GetTrainMTBT(false)*keyValuePairs["W11A"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W11A"].Item2 , LoadedTrains = keyValuePairs["W11A"].Item2 ,EmptyTrains = keyValuePairs["W11A"].Item1 },
                            new Mch {Id = auxId++, InstallLocation = "W11B", MTBT = GetTrainMTBT(false)*keyValuePairs["W11B"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W11B"].Item2 , LoadedTrains = keyValuePairs["W11B"].Item2 , EmptyTrains = keyValuePairs["W11B"].Item1 },
                            new Mch {Id = auxId++, InstallLocation = "W11C", MTBT = GetTrainMTBT(false)*keyValuePairs["W11C"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W11C"].Item2 , LoadedTrains = keyValuePairs["W11C"].Item2 , EmptyTrains = keyValuePairs["W11C"].Item1 }
                            }
                        },
                        new Amv{AmvNumber = 2, MchsInAmv = new List<Mch>(){ //AMV2
                            new Mch{Id = auxId++, InstallLocation = "W21A", MTBT = GetTrainMTBT(false)*keyValuePairs["W21A"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W21A"].Item2 , LoadedTrains = keyValuePairs["W21A"].Item2, EmptyTrains = keyValuePairs["W21A"].Item1},
                            new Mch{Id = auxId++, InstallLocation = "W21B", MTBT = GetTrainMTBT(false)*keyValuePairs["W21B"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W21B"].Item2 , LoadedTrains = keyValuePairs["W21B"].Item2, EmptyTrains = keyValuePairs["W21B"].Item1},
                            new Mch{Id = auxId++, InstallLocation = "W21C", MTBT = GetTrainMTBT(false)*keyValuePairs["W21C"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W21C"].Item2 , LoadedTrains = keyValuePairs["W21C"].Item2, EmptyTrains = keyValuePairs["W21C"].Item1},
                            }
                        },
                        new Amv{AmvNumber = 3, MchsInAmv = new List<Mch>(){ //AMV3
                            new Mch{Id = auxId++, InstallLocation = "W12A", MTBT = GetTrainMTBT(false)*keyValuePairs["W12A"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W12A"].Item2 , LoadedTrains = keyValuePairs["W12A"].Item2, EmptyTrains = keyValuePairs["W12A"].Item1},
                            new Mch{Id = auxId++, InstallLocation = "W12B", MTBT = GetTrainMTBT(false)*keyValuePairs["W12B"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W12B"].Item2 , LoadedTrains = keyValuePairs["W12B"].Item2, EmptyTrains =  keyValuePairs["W12B"].Item1},
                            new Mch{Id = auxId++, InstallLocation = "W12C", MTBT = GetTrainMTBT(false)*keyValuePairs["W12C"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W12C"].Item2 , LoadedTrains = keyValuePairs["W12C"].Item2, EmptyTrains = keyValuePairs["W12C"].Item1},
                            }
                        },
                        new Amv{AmvNumber = 4, MchsInAmv = new List<Mch>(){ //AMV4
                            new Mch{Id = auxId++, InstallLocation = "W22A", MTBT = GetTrainMTBT(false)*keyValuePairs["W22A"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W22A"].Item2 , LoadedTrains = keyValuePairs["W22A"].Item2, EmptyTrains = keyValuePairs["W22A"].Item1},
                            new Mch{Id = auxId++, InstallLocation = "W22B", MTBT = GetTrainMTBT(false)*keyValuePairs["W22B"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W22B"].Item2 , LoadedTrains = keyValuePairs["W22B"].Item2, EmptyTrains = keyValuePairs["W22B"].Item1},
                            new Mch{Id = auxId++, InstallLocation = "W22C", MTBT = GetTrainMTBT(false)*keyValuePairs["W22C"].Item1 +
                            GetTrainMTBT(true)* keyValuePairs["W22C"].Item2 , LoadedTrains = keyValuePairs["W22C"].Item2, EmptyTrains = keyValuePairs["W22C"].Item1},

                            }
                        }
                      }
                  });
                DebugLog.Logar(segment.Location +
                               " " + keyValuePairs["W11A"].Item1 + " " + keyValuePairs["W11A"].Item2 +
                               " " + keyValuePairs["W21A"].Item1 + " " + keyValuePairs["W21A"].Item2 +
                               " " + keyValuePairs["W12A"].Item1 + " " + keyValuePairs["W12A"].Item2 +
                               " " + keyValuePairs["W22A"].Item1 + " " + keyValuePairs["W22A"].Item2);
            }

            sgfContext.Dispose();
            return tu;
        }

        private static List<TrainMovSegments> GetTrainsBySegment(SgfContext sgfContext, int location, DateTime initialDate,
                                                                 DateTime finalDate)
        {
            return sgfContext.TrainMovSegments.Include(x => x.Train).
                    Where(x => x.Segment == "WT" && x.Train.TrainName.StartsWith("M") &&
                    x.Location == location && x.OcupationDate >= initialDate && x.OcupationDate <= finalDate).ToList();
        }

        private static double GetTrainMTBT(bool isLoaded) 
        {
            if (isLoaded)
            {
                return (Constants.LocomotivesInTrain * (Constants.LocomotiveTare) + Constants.WagonsInTrain * ((Constants.LoadedWagonGDT +
                                Constants.LoadedWagonGDU) / 2 + Constants.WagonTare)) / Math.Pow(10, 6);
            }
            else
            {
                return (Constants.LocomotivesInTrain * (Constants.LocomotiveTare) + Constants.WagonsInTrain * (Constants.WagonTare)) / Math.Pow(10, 6);
            }
        }

        private static List<string> GetMchByOccupations(List<int> tracks, int direction, TU.TypesOfTU type)
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
            else if (tracks.Count == 2) // TU A
            {
                if (type == TU.TypesOfTU.A)
                {
                    myList = myList.Concat(amv_3).Concat(amv_4).ToList();

                    if (tracks[0] == 1 && tracks[1] == 2)
                        myList = direction == 1 ? myList = myList.Concat(amv_1).ToList() :
                                                  myList = myList.Concat(amv_2).ToList();
                    else
                        myList = direction == 1 ? myList = myList.Concat(amv_2).ToList() :
                                                  myList = myList.Concat(amv_1).ToList();
                }
                else // TU V
                {
                    myList = myList.Concat(amv_1).Concat(amv_2).ToList();

                    if (tracks[0] == 1 && tracks[1] == 2)
                        myList = direction == 1 ? myList = myList.Concat(amv_4).ToList() :
                                                  myList = myList.Concat(amv_3).ToList();
                    else
                        myList = direction == 1 ? myList = myList.Concat(amv_3).ToList() :
                                                  myList = myList.Concat(amv_4).ToList();
                }
            }
            return myList;
        }
    }
}
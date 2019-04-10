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
        private static readonly string[] machines = new string[] {
            "W11A", "W11B", "W11C", "W21A", "W21B", "W21C", "W12A", "W12B", "W12C", "W22A", "W22B", "W22C"
        };


        public static List<TU> GetTu(DateTime initialDate, DateTime finalDate, int initialKM = 0, int finalKM = 892) //Return the list of TUs
        {
            var sgfContext = new SgfContext();
            var segments = sgfContext.Segments.Where(x => x.Segment == "WT" && x.Location >= initialKM &&
                           x.Location <= finalKM).OrderBy(x => x.Location).ToList();
            var tu = new List<TU>();

            foreach (var segment in segments)
            {
                var trainBySegmentObjects = GetTrainsBySegment(sgfContext, segment.Location, initialDate, finalDate);
                int trainBySegment = trainBySegmentObjects.Count;
                var trains = trainBySegmentObjects.Select(x => x.TrainID).Distinct();
                Dictionary<string, Tuple<List<TrainMovSegments>, List<TrainMovSegments>>> keyValuePairs = new Dictionary<string, Tuple<List<TrainMovSegments>, List<TrainMovSegments>>>();
                Dictionary<int, double> weightByAmv = new Dictionary<int, double>();
                machines.ToList().ForEach(x => keyValuePairs.Add(x, new Tuple<List<TrainMovSegments>, List<TrainMovSegments>>(new List<TrainMovSegments>(), new List<TrainMovSegments>())));
                for (int i = 1; i <= 4; i++)
                {
                    weightByAmv.Add(i, new Double());
                }

                foreach (var train in trains)
                {
                    var trainMov = trainBySegmentObjects.Where(x => x.TrainID == train).OrderBy(x => x.OcupationDate);
                    var trainOS = trainMov.FirstOrDefault().Train.OS;
                    var direction = trainMov.FirstOrDefault().Direction;
                    var tracks = trainMov.Select(x => x.Track).ToList();
                    TU.TypesOfTU type = Constants.TUTypeA.Contains(segment.Location) ? TU.TypesOfTU.A : TU.TypesOfTU.V;
                    var mchs = GetMchByOccupations(tracks, direction, type);
                    var amvs = GetAmvByOccupations(tracks, direction, type);
                    var compositions = GetCompositionsByTrain(sgfContext, trainOS);
                    var weight = compositions.Sum(x => x.IndividualWeight)/Math.Pow(10,6);
                    //DebugLog.Logar(compositions.Count.ToString());
                    amvs.ForEach(x =>
                    {
                        weightByAmv[x] += weight;
                    });
                    mchs.ForEach(x =>
                    {
                        var emptyTrains = keyValuePairs[x].Item1;
                        emptyTrains.AddRange(trainMov.Where(y => y.Direction == 1));
                        var loadedTrains = keyValuePairs[x].Item2;
                        loadedTrains.AddRange(trainMov.Where(y => y.Direction == -1));
                        keyValuePairs[x] = new Tuple<List<TrainMovSegments>, List<TrainMovSegments>>(emptyTrains, loadedTrains);
                    });
                }

                tu.Add(
                  new TU
                  {
                      Km = segment.Location,
                      TypeOfTU = Constants.TUTypeA.Contains(segment.Location) ? TU.TypesOfTU.A : TU.TypesOfTU.V,
                      AmvsInTU = new List<Amv>()
                      {
                        new Amv
                        {
                            AmvNumber = 1,
                            MchsInAmv = new List<Mch>()
                            { //AMV1
                                new Mch
                                {
                                    InstallLocation = "W11A",
                                    MTBT = weightByAmv[1],
                                    LoadedTrains = keyValuePairs["W11A"].Item2,
                                    EmptyTrains = keyValuePairs["W11A"].Item1
                                },
                                new Mch
                                {
                                    InstallLocation = "W11B",
                                    MTBT = weightByAmv[1],
                                    LoadedTrains = keyValuePairs["W11B"].Item2,
                                    EmptyTrains = keyValuePairs["W11B"].Item1
                                },
                                new Mch
                                {
                                    InstallLocation = "W11C",
                                    MTBT = weightByAmv[1],
                                    LoadedTrains = keyValuePairs["W11C"].Item2,
                                    EmptyTrains = keyValuePairs["W11C"].Item1
                                }
                            }
                        },
                        new Amv
                        {
                            AmvNumber = 2,
                            MchsInAmv = new List<Mch>()
                            { //AMV2
                                new Mch
                                {
                                    InstallLocation = "W21A",
                                    MTBT = weightByAmv[2],
                                    LoadedTrains = keyValuePairs["W21A"].Item2,
                                    EmptyTrains = keyValuePairs["W21A"].Item1
                                },
                                new Mch
                                {
                                    InstallLocation = "W21B",
                                    MTBT = weightByAmv[2],
                                    LoadedTrains = keyValuePairs["W21B"].Item2,
                                    EmptyTrains = keyValuePairs["W21B"].Item1
                                },
                                new Mch
                                {
                                    InstallLocation = "W21C",
                                    MTBT = weightByAmv[2],
                                    LoadedTrains = keyValuePairs["W21C"].Item2,
                                    EmptyTrains = keyValuePairs["W21C"].Item1
                                }
                             }
                        },
                        new Amv
                        {
                            AmvNumber = 3,
                            MchsInAmv = new List<Mch>()
                            { //AMV3
                                new Mch
                                {
                                    InstallLocation = "W12A",
                                    MTBT = weightByAmv[3],
                                    LoadedTrains = keyValuePairs["W12A"].Item2,
                                    EmptyTrains = keyValuePairs["W12A"].Item1
                                },
                                new Mch
                                {
                                    InstallLocation = "W12B",
                                    MTBT = weightByAmv[3],
                                    LoadedTrains = keyValuePairs["W12B"].Item2,
                                    EmptyTrains =  keyValuePairs["W12B"].Item1
                                },
                                new Mch
                                {
                                    InstallLocation = "W12C",
                                    MTBT = weightByAmv[3],
                                    LoadedTrains = keyValuePairs["W12C"].Item2,
                                    EmptyTrains = keyValuePairs["W12C"].Item1
                                }
                            }
                        },
                        new Amv
                        {
                            AmvNumber = 4,
                            MchsInAmv = new List<Mch>()
                            { //AMV4
                                new Mch
                                {
                                    InstallLocation = "W22A",
                                    MTBT = weightByAmv[4],
                                    LoadedTrains = keyValuePairs["W22A"].Item2,
                                    EmptyTrains = keyValuePairs["W22A"].Item1
                                },
                                new Mch
                                {
                                    InstallLocation = "W22B",
                                    MTBT = weightByAmv[4],
                                    LoadedTrains = keyValuePairs["W22B"].Item2,
                                    EmptyTrains = keyValuePairs["W22B"].Item1
                                },
                                new Mch
                                {
                                    InstallLocation = "W22C",
                                    MTBT = weightByAmv[4],
                                    LoadedTrains = keyValuePairs["W22C"].Item2,
                                    EmptyTrains = keyValuePairs["W22C"].Item1
                                }
                            }
                        }
                      }
                  });
            }

            sgfContext.Dispose();
            return tu;
        }

        public static List<TrainMovSegments> GetTrainsBySegment(SgfContext sgfContext, int location, DateTime initialDate,
                                                                 DateTime finalDate) // return the list of train movement by segments from the DB 
        {
            List<TrainMovSegments> trainMovSegments = new List<TrainMovSegments>();

            try
            {
                trainMovSegments = sgfContext.TrainMovSegments.Include(x => x.Train).
                        Where(x => x.Segment == "WT" && x.OcupationDate >= initialDate && x.OcupationDate <= finalDate &&
                        x.Train.TrainName.StartsWith("M") && x.Location == location).ToList();
            }
            catch (Exception e)
            {
                DebugLog.Logar("GetTrainsBySegment: " + e.Message);
            }
            return trainMovSegments;
        }

        private static List<Composition> GetCompositionsByTrain(SgfContext sgfContext, string OS)
        {
            List<Composition> compositions = new List<Composition>();
            try
            {
                compositions = sgfContext.Compositions.Where(x => x.OS == OS).ToList();
            }
            catch (Exception e)
            {
                DebugLog.Logar("GetCompositionsByTrain: " + e.Message);
            }

            return compositions;
        }

        private static List<string> GetMchByOccupations(List<int> tracks, int direction, TU.TypesOfTU type)//return a list of string with the name of the mchs
        {     //Add the followings MCHs to the respective AMV
            var amv_1 = new List<string>() { "W11A", "W11B", "W11C" };
            var amv_2 = new List<string>() { "W21C", "W21B", "W21A" };
            var amv_3 = new List<string>() { "W12C", "W12B", "W12A" };
            var amv_4 = new List<string>() { "W22A", "W22B", "W22C" };
            var myList = new List<string>();

            if (tracks.Count == 1 || tracks.Distinct().Count() == 1)//If there's just one element on the track list, or a list of the same direction in the list
            {
                if (tracks[0] == 1) // if the track list only has 1 or 2, it means the trains didnt change the track
                    myList = myList.Concat(amv_1).Concat(amv_2).ToList(); //stayed on track 1
                else if (tracks[0] == 2)
                    myList = myList.Concat(amv_3).Concat(amv_4).ToList(); //stayed on track 2
            }
            else if (tracks.Count == 2) // this means the train has changed the track to line 1 to 2, or reverse.
            {
                if (type == TU.TypesOfTU.A) // check if it is a TU of type A
                {
                    myList = myList.Concat(amv_3).Concat(amv_4).ToList();//if it is type A, consequently in all the changes it'll pass on the amv 3 and 4

                    if (tracks[0] == 1 && tracks[1] == 2) // if it changed from line 1 to line 2 
                        myList = direction == 1 ? myList = myList.Concat(amv_1).ToList() : // if the direction is 1 it's going towards the mine
                                                  myList = myList.Concat(amv_2).ToList();  // else the direction will be -1 it's going towards the port
                    else                                  // if it changed from line 2 to line 1
                        myList = direction == 1 ? myList = myList.Concat(amv_2).ToList() : // if the direction is 1 it's going towards the mine
                                                  myList = myList.Concat(amv_1).ToList(); // else the direction will be -1 it's going towards the port
                }
                else // if it's not type A it is type V
                {
                    myList = myList.Concat(amv_1).Concat(amv_2).ToList();//if it is type V, consequently in all the changes it'll pass on the amv 1 and 2
                                                                         //this section is the same as above
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

        private static List<int> GetAmvByOccupations(List<int> tracks, int direction, TU.TypesOfTU type)//return a list of string with the name of the mchs
        {     //Add the followings MCHs to the respective AMV
            var myList = new List<int>();

            if (tracks.Count == 1 || tracks.Distinct().Count() == 1)//If there's just one element on the track list, or a list of the same direction in the list
            {
                if (tracks[0] == 1) // if the track list only has 1 or 2, it means the trains didnt change the track
                    myList.AddRange(new int[] { 1, 2 });  //stayed on track 1
                else if (tracks[0] == 2)
                    myList.AddRange(new int[] { 3, 4 }); //stayed on track 2
            }
            else if (tracks.Count == 2) // this means the train has changed the track to line 1 to 2, or reverse.
            {
                if (type == TU.TypesOfTU.A) // check if it is a TU of type A
                {
                    myList.AddRange(new int[] { 3, 4 });//if it is type A, consequently in all the changes it'll pass on the amv 3 and 4

                    if (tracks[0] == 1 && tracks[1] == 2) // if it changed from line 1 to line 2 
                        if (direction == 1) myList.Add(1); // if the direction is 1 it's going towards the mine
                        else myList.Add(2);  // else the direction will be -1 it's going towards the port
                    else                                  // if it changed from line 2 to line 1
                        if (direction == 1) myList.Add(2); // if the direction is 1 it's going towards the mine
                    else myList.Add(1); // else the direction will be -1 it's going towards the port
                }
                else // if it's not type A it is type V
                {
                    myList.AddRange(new int[] { 1, 2 });//if it is type V, consequently in all the changes it'll pass on the amv 1 and 2
                                                        //this section is the same as above
                    if (tracks[0] == 1 && tracks[1] == 2)
                        if (direction == 1) myList.Add(4);
                        else myList.Add(3);
                    else
                        if (direction == 1) myList.Add(3);
                    else myList.Add(4);
                }
            }
            return myList;
        }
    }
}
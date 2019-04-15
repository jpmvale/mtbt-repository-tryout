using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MchRepositoryTryout.Services
{
    public static class Constants
    {
        public static readonly int WagonTare = 20;
        public static readonly int LocomotiveTare = 182;
        public static readonly int LoadedWagonGDT = 122;
        public static readonly int LoadedWagonGDU = 130;
        public static readonly int WagonsInTrain = 330;
        public static readonly int LocomotivesInTrain = 3;
        public static readonly List<int> TUTypeA = new List<int>() { 76, 85, 389, 396, 430, 441, 449 }; // Instantiate all the list of type A TUs
    }
}
﻿using MchRepositoryTryout.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace MchRepositoryTryout.DAL
{    
    public class SgfContext : DbContext
    {        
        public SgfContext() : base("name=SgfContext")
        {
              
        }

        public IDbSet<Segments> Segments { get; set; }
        public IDbSet<Train> Trains { get; set; }
        public IDbSet<TrainMovSegments> TrainMovSegments { get; set; }
    }
}
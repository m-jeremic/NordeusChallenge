using BackendAndGameDesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAndGameDesign.ViewModels
{
    public class ClubAndMatchupData
    {
        public List<Club> Clubs { get; set; }

        public List<Matchup> Matchups { get; set; }
    }
}

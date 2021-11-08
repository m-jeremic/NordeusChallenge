using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAndGameDesign.Models
{
    public class Matchup
    {       
        public Club TeamA { get; set; }

        public Club TeamB { get; set; }

        public int Result { get; set; }


    }
}

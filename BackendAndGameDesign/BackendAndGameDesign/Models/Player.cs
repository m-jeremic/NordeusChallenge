using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAndGameDesign.Models
{
    public class Player
    {
        public long Id { get; set; }

        public string Name { get; set; } 
        
        public int Rating { get; set; }

        public long ClubId { get; set; }

        public Club Club { get; set; }

    }
}

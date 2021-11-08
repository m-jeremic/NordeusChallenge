using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAndGameDesign.Models
{
    public class Club
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double ScoredGoals { get; set; }

        public double ConcededGoals { get; set; }

        public double RedCards { get; set; }

        public double YellowCards { get; set; }

        public double Rating { get; set; }

        public byte isDefault { get; set; }

        public virtual IEnumerable<Player> Players { get; set; } = new List<Player>();


    }
}

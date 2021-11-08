using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAndGameDesign.Models
{
    public interface IRepository
    {

        Club GetClub(long id);

        IEnumerable<Club> GetClubs(int allClubs = 0);

        void AddClub(Club club);

        void UpdateClub(Club club);

        void DeleteClub(long id);
    }
}

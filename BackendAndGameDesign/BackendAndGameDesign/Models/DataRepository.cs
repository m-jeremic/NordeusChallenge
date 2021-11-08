using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAndGameDesign.Models
{
    public class DataRepository : IRepository
    {
        public ClubsDbContext context;

        public DataRepository(ClubsDbContext ctx)
        {
            context = ctx;
        }

        public Club GetClub(long id)
        {
            Club club = context.Clubs.Include(cl => cl.Players).Where(cl => cl.Id == id).First();
            return club;
        }

        public IEnumerable<Club> GetClubs(int allClubs = 0)
        {
            IEnumerable<Club> clubs = context.Clubs.OrderByDescending(clb => clb.Rating).ToList();

            if (allClubs == 1)
            {
                clubs = clubs.Where(clb => clb.isDefault == 0);
            }

            if (allClubs == 2)
            {
                clubs = clubs.Where(clb => clb.isDefault == 1);
            }

            return clubs;
        }

        public void AddClub(Club club)
        {
            context.Clubs.Add(club);
            context.SaveChanges();
        }

        public void UpdateClub(Club club)
        {
            context.Clubs.Update(club);
            context.SaveChanges();
        }

        public void DeleteClub(long id)
        {
            Club club = context.Clubs.Where(clb => clb.Id == id).First();
            context.Remove(club);
            context.SaveChanges();
        }

    }
}

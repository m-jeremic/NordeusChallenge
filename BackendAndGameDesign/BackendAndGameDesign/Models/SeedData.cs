using BackendAndGameDesign.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAndGameDesign.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(ClubsDbContext context)
        {
            context.Database.Migrate();
            if (context.Clubs.Count() == 0 || context.Players.Count() == 0)
            {
                Club club;
                List<Club> clubs = new List<Club>();
                for (int i = 0; i < 8; i++)
                {
                    club = new Club();
                    club.Name = $"Team{i + 1}";
                    club.isDefault = 0;
                    context.Clubs.Add(club);
                    clubs.Add(club);
                }
                context.SaveChanges();

                for (int i = 0; i < context.Clubs.Count(); i++)
                {
                    Random rnd = new Random();
                    Player player;
                    int numberOfPlayers = rnd.Next(18, 30);
                    for (int j = 0; j < numberOfPlayers; j++)
                    {
                        player = new Player();
                        player.Name = $"Player{j + 1}";
                        player.Rating = rnd.Next(100);
                        player.ClubId = clubs[i].Id;
                        context.Players.Add(player);
                    }
                }
                context.SaveChanges();

                foreach (Club team in context.Clubs)
                {
                    Calculations.FillClubProperties(team, context);
                }
                context.SaveChanges();
            }
        }
    }
}

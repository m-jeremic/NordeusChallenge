using BackendAndGameDesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAndGameDesign.Infrastructure
{
    public static class Calculations
    {
        public static List<Matchup> CreateMatchups(IEnumerable<Club> clubs)
        {
            List<Matchup> matchups = new List<Matchup>();
            List<Club> drawClubs = new List<Club>();
            foreach (Club club in clubs)
            {
                drawClubs.Add(club);
            }
            List<Club> testedClubs = new List<Club>();
            Matchup matchup;
            Random rnd = new Random();
            int index1 = 0;
            int index2;
            while (drawClubs.Count() > 1)
            {
                matchup = new Matchup();
                matchup.TeamA = drawClubs[index1];
                index2 = rnd.Next(1, drawClubs.Count() - 1);
                if (drawClubs[index1].Rating - drawClubs[index2].Rating <= 10)
                {                   
                    matchup.TeamB = drawClubs[index2];
                    matchups.Add(matchup);
                    drawClubs.RemoveAt(index2);
                    drawClubs.RemoveAt(index1);
                    testedClubs = new List<Club>();
                }
                else
                {
                    if (!testedClubs.Contains(drawClubs[index2]))
                    {
                        testedClubs.Add(drawClubs[index2]);
                    }
                    
                    if (testedClubs.Count() == drawClubs.Count() - 1)
                    {
                        matchup.TeamB = null;
                        matchups.Add(matchup);
                        drawClubs.RemoveAt(index1);
                        testedClubs = new List<Club>();
                    }
                }
            }

            if (drawClubs.Count() == 1)
            {
                matchup = new Matchup();
                matchup.TeamA = drawClubs[0];
                matchup.TeamB = null;
                matchups.Add(matchup);
            }

            foreach (Matchup matchup1 in matchups)
            {
                CreateResult(matchup1);
            }

            return matchups;
        }

        public static void FillClubProperties(Club club, ClubsDbContext ctx)
        {
            Random rnd = new Random();
            club.Rating = Math.Round((double)ctx.Players.Where(player => player.ClubId == club.Id).Select(player => player.Rating).Sum() / ctx.Players.Where(player => player.ClubId == club.Id).Count());// sum of players' ratings divided by number of players, rounded to integer value => better teams have higher rating!!!
            club.ScoredGoals = Math.Round(rnd.NextDouble() * 5 * (club.Rating / 100), 2);// random between 0 and 1 multiplied with max number of 5 goals (arbitrary value), multiplied with club rating divided by 100, rounded to two decimal places => better teams score more goals!!!
            club.ConcededGoals = Math.Round(rnd.NextDouble() * 2 / (club.Rating / 100), 2);// random between 0 and 1 multiplied with max number of 2 goals (arbitrary value), divided with club rating divided by 100, rounded to two decimal places => better teams concede fewer goals!!!
            club.RedCards = Math.Round(rnd.NextDouble() * 1.5 / (club.Rating / 100), 2);// random between 0 and 1 multiplied with max number of 1.5 red cards (arbitrary value), divided with club rating divided by 100, rounded to two decimal places => better teams get fewer red cards!!!
            club.YellowCards = Math.Round(rnd.NextDouble() * 4 / (club.Rating / 100), 2);// random between 0 and 1 multiplied with max number of 4 yellow cards (arbitrary value), divided with club rating divided by 100, rounded to two decimal places => better teams get fewer yellow cards!!!
        }

        public static void CreateResult(Matchup matchup)
        {
            // check if teamB is null
            if (matchup.TeamB == null)
            {
                // if yes teamA is winner, result = 1
                matchup.Result = 1;
                    return;
            }

            // result of a match can be : 1 - TeamA win, 0 - draw , 2 - TeamB wins
            // result is calculated as difference between number of scored goals by teamA and teamB,
            // whichever team score more goals wins, unless difference is less then or equal to 0.3, then its draw

            // first, scoredGoalsPerGame is decreased by number of redCardsPerGame multiplied by 0.08, and yellowCardsPerGame multiplied by 0.03
            double scoredGoalsByTeamA = matchup.TeamA.ScoredGoals - matchup.TeamA.RedCards * 0.08 - matchup.TeamA.YellowCards * 0.03;
            double scoredGoalsByTeamB = matchup.TeamB.ScoredGoals - matchup.TeamB.RedCards * 0.08 - matchup.TeamB.YellowCards * 0.03;

            // also, concededGoalsPerGame is increased by number of redCardsPerGame multiplied by 0.08, and yellowCardsPerGame multiplied by 0.03
            double concededdGoalsByTeamA = matchup.TeamA.ConcededGoals + matchup.TeamA.RedCards * 0.08 + matchup.TeamA.YellowCards * 0.03;
            double concededGoalsByTeamB = matchup.TeamB.ConcededGoals + matchup.TeamB.RedCards * 0.08 + matchup.TeamB.YellowCards * 0.03;

            // then, scored goals by teamA is calculated as sum of its decreased scoredGoalsPerGame and teamB's increased soncedeGoalsPerGame, divided by two
            // same is done for teamB
            double goalsByTeamA = (scoredGoalsByTeamA + concededGoalsByTeamB) / 2;
            double goalsByTeamB = (scoredGoalsByTeamB + concededdGoalsByTeamA) / 2;

            // finally, scoredGoals by teamA only is increased by difference in ratings multiplied with 0.05
            goalsByTeamA += (matchup.TeamA.Rating - matchup.TeamB.Rating) * 0.05;

            bool isDraw = Math.Abs(goalsByTeamA - goalsByTeamB) <= 0.3;
            if (isDraw)
            {
                matchup.Result = 0;
            }
            else
            {
                if (goalsByTeamA > goalsByTeamB)
                {
                    matchup.Result = 1;
                }
                else
                {
                    matchup.Result = 2;
                }
            }


        }
    }
}

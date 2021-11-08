using BackendAndGameDesign.Infrastructure;
using BackendAndGameDesign.Models;
using BackendAndGameDesign.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAndGameDesign.Controllers
{
    public class HomeController : Controller
    {
        public IRepository repository;

        public HomeController(IRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index()
        {
            IEnumerable<Club> clubs = repository.GetClubs();
            return View(clubs);
        }

        public IActionResult Edit(long id = 0)
        {
            Club club = new Club();
            if (id != 0)
            {
                club = repository.GetClub(id);
            }
            return View(club);
        }

        [HttpPost]
        public IActionResult Edit(Club club)
        {
            List<Player> newPlayers = new List<Player>();
            foreach (Player player in club.Players)
            {
                if (!String.IsNullOrWhiteSpace(player.Name) && player.Rating != 0)
                {
                    newPlayers.Add(player);
                }
            }
            club.Players = newPlayers;
            
            if (club.Id == 0)
            {
                club.isDefault = 1;
                repository.AddClub(club);
            }
            else
            {
                repository.UpdateClub(club);
            }
            return RedirectToAction("Index");
        }

        public IActionResult CreateMatchups(int allClubs)
        {
            IEnumerable<Club> clubs = repository.GetClubs(allClubs);
            List<Matchup> matchups = Calculations.CreateMatchups(clubs);
            return View("Matchups", matchups);

        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            repository.DeleteClub(id);
            return RedirectToAction("Index");
        }

    }
}

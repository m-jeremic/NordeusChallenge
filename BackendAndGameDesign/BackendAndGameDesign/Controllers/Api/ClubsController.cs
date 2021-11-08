using BackendAndGameDesign.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAndGameDesign.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubsController : ControllerBase
    {
        public IRepository repository;

        public ClubsController(IRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IActionResult GetClubs()
        {
            IEnumerable<Club> clubs = repository.GetClubs();
            foreach (Club club in clubs)
            {
                club.Players = null;
            }
            return Ok(clubs);
        }

        [HttpGet("{id}")]
        public IActionResult GetClub(long id)
        {
            Club club = repository.GetClub(id);
            foreach (Player player in club.Players)
            {
                player.Club = null;
            }
            return Ok(club);
        }
    }
}

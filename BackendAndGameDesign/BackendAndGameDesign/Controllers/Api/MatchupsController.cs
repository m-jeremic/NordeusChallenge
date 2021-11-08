using BackendAndGameDesign.Infrastructure;
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
    public class MatchupsController : ControllerBase
    {
        public IRepository repository;

        public MatchupsController(IRepository repo)
        {
            repository = repo;
        }

        [HttpGet("{allClubs}")]
        public IActionResult GetMatchups(int allClubs = 0)
        {
            IEnumerable<Club> clubs = repository.GetClubs(allClubs);
            List<Matchup> matchups = Calculations.CreateMatchups(clubs);
            return Ok(matchups);
        }
    }
}

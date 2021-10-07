using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nfl_rushing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nfl_rushing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerStatsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PlayerStatsController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Tuple<List<PlayerStats>, int, int> Get(string sortBy, string search, int page, int pageSize)
        {
            var players = _context.PlayerStats.AsQueryable();
            var total = players.Count();

            players = players.AddSearch(search);
            players = players.AddSort(sortBy);
            

            return Tuple.Create(players.Skip(pageSize * (page -1)).Take(pageSize).ToList(), players.Count(), total);
        }
    }
}

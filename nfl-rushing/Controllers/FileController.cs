using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using nfl_rushing.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace nfl_rushing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FileController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Download(string sortBy, string search)
        {
            var players = _context.PlayerStats.AsQueryable();

            players = players.AddSearch(search);
            players = players.AddSort(sortBy);

            using (StringWriter sw = new StringWriter())
            {
                using (CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture))
                {
                    cw.WriteHeader<PlayerStats>();
                    cw.NextRecord();
                    foreach (PlayerStats player in players)
                    {
                        cw.WriteRecord(player);
                        cw.NextRecord();
                    }
                }

                return File(Encoding.UTF8.GetBytes(sw.ToString()), "text/csv", "PlayerStats.csv");
            }
        }
    }
}

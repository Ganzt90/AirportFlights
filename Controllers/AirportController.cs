using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirportFlights.Data;
using AirportFlights.Models;

namespace AirportFlights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly DemoContext _context;

        public AirportController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/Airport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airport>>> GetAirports()
        {
          if (_context.Airports == null)
          {
              return NotFound();
          }
          var result = await _context.Airports.ToListAsync();
            return result;
        }


        private bool AirportExists(string id)
        {
            return (_context.AirportsData?.Any(e => e.AirportCode == id)).GetValueOrDefault();
        }
    }
}

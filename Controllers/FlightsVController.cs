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
    public class FlightsVController : ControllerBase
    {
        private readonly DemoContext _context;

        public FlightsVController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/FlightsV
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightsV>>> GetFlightsVsData()
        {
          if (_context.FlightsVs == null)
          {
              return NotFound();
          }
            var result = await _context.FlightsVs.ToListAsync();
            return result;
        }


        private bool FlightsVExists(int id)
        {
            return (_context.FlightsVs?.Any(e => e.FlightId == id)).GetValueOrDefault();
        }
    }
}

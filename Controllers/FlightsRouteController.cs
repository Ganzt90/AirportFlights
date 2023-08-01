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
    public class FlightsRouteController : ControllerBase
    {
        private readonly DemoContext _context;

        public FlightsRouteController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/FlightsRoute
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightsRoute>>> GetFlightsRoutes()
        {
            if (_context.FlightsRoutes == null)
            {
                return NotFound();
            }
            var result = await _context.FlightsRoutes.ToListAsync();
            return result;
        }


        private bool FlightsRouteExists(string id)
        {
            return (_context.FlightsRoutes?.Any(e => e.FlightNo == id)).GetValueOrDefault();
        }
    }
}

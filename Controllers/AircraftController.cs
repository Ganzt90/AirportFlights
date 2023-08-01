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
    public class AircraftController : ControllerBase
    {
        private readonly DemoContext _context;

        public AircraftController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/Aircraft
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aircraft>>> GetAircrafts()
        {
          if (_context.Aircrafts == null)
          {
              return NotFound();
          }
          var result = await _context.Aircrafts.ToListAsync();
            return result;
        }


        private bool AircraftExists(string id)
        {
            return (_context.AircraftsData?.Any(e => e.AircraftCode == id)).GetValueOrDefault();
        }
    }
}

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
    public class AirportsDatumController : ControllerBase
    {
        private readonly DemoContext _context;

        public AirportsDatumController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/AirportsDatum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirportsDatum>>> GetAirportsData()
        {
          if (_context.AirportsData == null)
          {
              return NotFound();
          }
            return await _context.AirportsData.ToListAsync();
        }

        // GET: api/AirportsDatum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirportsDatum>> GetAirportsDatum(string id)
        {
          if (_context.AirportsData == null)
          {
              return NotFound();
          }
            var airportsDatum = await _context.AirportsData.FindAsync(id);

            if (airportsDatum == null)
            {
                return NotFound();
            }

            return airportsDatum;
        }

        // PUT: api/AirportsDatum/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirportsDatum(string id, AirportsDatum airportsDatum)
        {
            if (id != airportsDatum.AirportCode)
            {
                return BadRequest();
            }

            _context.Entry(airportsDatum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirportsDatumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AirportsDatum
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AirportsDatum>> PostAirportsDatum(AirportsDatum airportsDatum)
        {
          if (_context.AirportsData == null)
          {
              return Problem("Entity set 'DemoContext.AirportsData'  is null.");
          }
            _context.AirportsData.Add(airportsDatum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AirportsDatumExists(airportsDatum.AirportCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAirportsDatum", new { id = airportsDatum.AirportCode }, airportsDatum);
        }

        // DELETE: api/AirportsDatum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirportsDatum(string id)
        {
            if (_context.AirportsData == null)
            {
                return NotFound();
            }
            var airportsDatum = await _context.AirportsData.FindAsync(id);
            if (airportsDatum == null)
            {
                return NotFound();
            }

            _context.AirportsData.Remove(airportsDatum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirportsDatumExists(string id)
        {
            return (_context.AirportsData?.Any(e => e.AirportCode == id)).GetValueOrDefault();
        }
    }
}

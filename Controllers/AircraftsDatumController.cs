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
    public class AircraftsDatumController : ControllerBase
    {
        private readonly DemoContext _context;

        public AircraftsDatumController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/AircraftsDatum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AircraftsDatum>>> GetAircraftsData()
        {
          if (_context.AircraftsData == null)
          {
              return NotFound();
          }
            return await _context.AircraftsData.ToListAsync();
        }

        // GET: api/AircraftsDatum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AircraftsDatum>> GetAircraftsDatum(string id)
        {
          if (_context.AircraftsData == null)
          {
              return NotFound();
          }
            var aircraftsDatum = await _context.AircraftsData.FindAsync(id);

            if (aircraftsDatum == null)
            {
                return NotFound();
            }

            return aircraftsDatum;
        }

        // PUT: api/AircraftsDatum/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAircraftsDatum(string id, AircraftsDatum aircraftsDatum)
        {
            if (id != aircraftsDatum.AircraftCode)
            {
                return BadRequest();
            }

            _context.Entry(aircraftsDatum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AircraftsDatumExists(id))
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

        // POST: api/AircraftsDatum
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AircraftsDatum>> PostAircraftsDatum(AircraftsDatum aircraftsDatum)
        {
          if (_context.AircraftsData == null)
          {
              return Problem("Entity set 'DemoContext.AircraftsData'  is null.");
          }
            _context.AircraftsData.Add(aircraftsDatum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AircraftsDatumExists(aircraftsDatum.AircraftCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAircraftsDatum", new { id = aircraftsDatum.AircraftCode }, aircraftsDatum);
        }

        
        // DELETE: api/AircraftsDatum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAircraftsDatum(string id)
        {
            if (_context.AircraftsData == null)
            {
                return NotFound();
            }
            var aircraftsDatum = await _context.AircraftsData.FindAsync(id);
            if (aircraftsDatum == null)
            {
                return NotFound();
            }

            _context.AircraftsData.Remove(aircraftsDatum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AircraftsDatumExists(string id)
        {
            return (_context.AircraftsData?.Any(e => e.AircraftCode == id)).GetValueOrDefault();
        }
    }
}

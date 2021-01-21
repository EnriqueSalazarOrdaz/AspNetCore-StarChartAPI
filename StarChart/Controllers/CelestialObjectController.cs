using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext parameters)
        {
            _context = parameters;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var records = _context.CelestialObjects.Where(e => e.Id == id).FirstOrDefault();
            if (records == null)
            {
                return NotFound();
            }
            records.OrbitalPeriod   = 
            return Ok()
        }
    }
}


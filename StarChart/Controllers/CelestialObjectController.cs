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
            var record = _context.CelestialObjects.Find(id);
            if (record == null)
                return NotFound();
            record.Satellites = _context.CelestialObjects.Where(co => co.Id == id).ToList();
            return Ok(record);
        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObject = _context.CelestialObjects.Find(name);
            if (celestialObject == null)
                return NotFound();
            celestialObject.Satellites = _context.CelestialObjects.Where(co => co.Name == name).ToList();
            return Ok(celestialObject);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList();
            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(co => co.Id == celestialObject.Id).ToList();
            }
            return Ok(celestialObjects);
        }
    }
}


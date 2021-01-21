using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

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
            var celestialObjects = _context.CelestialObjects.Where(co => co.Name == name).ToList();
            if (!celestialObjects.Any())
                return NotFound();
            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(co => co.Id == celestialObject.Id).ToList();
            }

            return Ok(celestialObjects);
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


        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject celestialObject)
        {
            _context.CelestialObjects.Add(celestialObject);
            _context.SaveChanges();
            return CreatedAtRoute("GetById", new { id = celestialObject.Id }, celestialObject);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CelestialObject celestialObject)
        {
            var celestialObjectFind = _context.CelestialObjects.Find(id);
            if (celestialObjectFind == null)
                return NotFound();
            celestialObjectFind.Name = celestialObject.Name;
            celestialObjectFind.OrbitalPeriod = celestialObject.OrbitalPeriod;
            celestialObjectFind.OrbitedObjectId = celestialObject.OrbitedObjectId;
            _context.CelestialObjects.Update(celestialObjectFind);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var celestialObjectF = _context.CelestialObjects.Find(id);
            if (celestialObjectF == null)
                return NotFound();
            celestialObjectF.Name = name;
            _context.CelestialObjects.Update(celestialObjectF);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var celestialObject = _context.CelestialObjects.Find(id);
            if (celestialObject == null)
                return NotFound();
            _context.CelestialObjects.Remove(celestialObject);
            _context.SaveChanges();
            return NoContent();
        }
    }
}


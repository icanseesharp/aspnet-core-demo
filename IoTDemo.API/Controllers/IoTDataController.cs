using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IoTDemo.API.Models;

namespace IoTDemo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/IoTData")]
    public class IoTDataController : Controller
    {
        private readonly IoTDemoDbContext _context;

        public IoTDataController(IoTDemoDbContext context)
        {
            _context = context;
        }

        // GET: api/IoTData
        [HttpGet]
        public IEnumerable<IoTData> GetIoTData()
        {
            return _context.IoTData;
        }

        // GET: api/IoTData/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIoTData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ioTData = await _context.IoTData.SingleOrDefaultAsync(m => m.Id == id);

            if (ioTData == null)
            {
                return NotFound();
            }

            return Ok(ioTData);
        }

        // PUT: api/IoTData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIoTData([FromRoute] int id, [FromBody] IoTData ioTData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ioTData.Id)
            {
                return BadRequest();
            }

            _context.Entry(ioTData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IoTDataExists(id))
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

        // POST: api/IoTData
        [HttpPost]
        public async Task<IActionResult> PostIoTData([FromBody] IoTData ioTData)
        {
            Console.WriteLine(DateTime.Now);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.IoTData.Add(ioTData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIoTData", new { id = ioTData.Id }, ioTData);
        }

        // DELETE: api/IoTData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIoTData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ioTData = await _context.IoTData.SingleOrDefaultAsync(m => m.Id == id);
            if (ioTData == null)
            {
                return NotFound();
            }

            _context.IoTData.Remove(ioTData);
            await _context.SaveChangesAsync();

            return Ok(ioTData);
        }

        private bool IoTDataExists(int id)
        {
            return _context.IoTData.Any(e => e.Id == id);
        }
    }
}
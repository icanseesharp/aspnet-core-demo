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
    public class IoTDataController : Controller
    {
        private readonly IoTDemoDbContext _context;

        public IoTDataController(IoTDemoDbContext context)
        {
            _context = context;
        }
        
        //GET: iotdata/getall
        [HttpGet]
        [Route("[Controller]/GetAll")]
        public IEnumerable<IoTData> GetIoTData()
        {
            return _context.IoTData;
        }
        
        //GET: iotdata/get?id={Id}
        [Route("[Controller]/Get")]
        public async Task<IActionResult> GetIoTData(int id)
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

        // POST: iotdata/write
        [HttpPost]
        [Route("[Controller]/write")]
        public async Task<IActionResult> PostIoTData(string name, string value, string date,string key)
        {

            #region check if the key is provided and valid
            var isValidKey = string.IsNullOrEmpty(key) ? false : _context.IoTKeys.Where(q => q.Key.ToString().ToLowerInvariant() == key).Count() > 0 ? true : false;          
            if(!isValidKey)
            {
                return Unauthorized();
            }
            #endregion

            var ioTData = new IoTData();            

            #region validate the provided data

            var existingName = string.IsNullOrEmpty(name) ? false : _context.IoTDataNames.Where(q=>q.Name.ToLowerInvariant() == name.ToLowerInvariant()).Count() > 0 ? true : false ;
            if(existingName)
            {
                ioTData.IoTDataNameId = _context.IoTDataNames.Where(q => q.Name.ToLowerInvariant() == name.ToLowerInvariant()).First().Id;
            }
            else
            {
                var newIotDataName = _context.Add(new IoTDataName() {
                    Name = name,
                    IoTKeyId = _context.IoTKeys.Where(q=>q.Key == Guid.Parse(key)).First().Id
                });
                ioTData.IoTDataNameId = newIotDataName.Entity.Id;
            }

            var ParsedDate = new DateTime();
            if (string.IsNullOrEmpty(date))
            {
                ioTData.Date = DateTime.Now;
            }
            else if (DateTime.TryParse(date, out ParsedDate))
            {
                ioTData.Date = ParsedDate;                
            }
            else
            {
                return BadRequest("Provided date is invalid, please check the date!");
            }
            


            if(string.IsNullOrEmpty(value))
            {
                return BadRequest("Please provide a floating point value");
            }
            var ioTValue = new float();
            if(float.TryParse(value, out ioTValue))
            {
                ioTData.Value = ioTValue;
            }
            else
            {
                return BadRequest("Value provided is not a valid floating point number");
            }
            #endregion

            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.IoTData.Add(ioTData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIoTData", new { id = ioTData.Id }, ioTData);
        }

        // DELETE: iotdata/delete?id={Id}
        [HttpDelete]
        [Route("[Controller]/delete")]
        public async Task<IActionResult> DeleteIoTData(int id)
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
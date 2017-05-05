using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IoTDemo.API.Models;
using System.Net;
using Microsoft.Extensions.Logging;

namespace IoTDemo.API.Controllers
{
    [Produces("application/json")]    
    public class IoTDataController : Controller
    {
        private readonly IoTDemoDbContext _context;
        readonly ILogger<IoTDataController> _log;

        public IoTDataController(IoTDemoDbContext context, ILogger<IoTDataController> log)
        {
            _context = context;
            _log = log;
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
                _log.LogWarning(string.Format("Invalid Id provided by user, provided value {0}", id.ToString()));
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, Please provide a valid integer Id to get the record"));
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
            if (!IsValidKey(key))
            {
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, could not auhtenticate the user with given key - The key is either invalid or disabled"));
            }
            #endregion

            var ioTData = new IoTData();

            #region validate the provided data

            var existingName = string.IsNullOrEmpty(name) ? false : _context.IoTDataNames.Where(q => q.Name.ToLowerInvariant() == name.ToLowerInvariant()).Count() > 0 ? true : false;
            if (existingName)
            {
                ioTData.IoTDataNameId = _context.IoTDataNames.Where(q => q.Name.ToLowerInvariant() == name.ToLowerInvariant()).First().Id;
            }
            else if(!string.IsNullOrEmpty(name))
            {
                var newIotDataName = _context.Add(new IoTDataName()
                {
                    Name = name,
                    IoTKeyId = _context.IoTKeys.Where(q => q.Key == Guid.Parse(key)).First().Id
                });
                ioTData.IoTDataNameId = newIotDataName.Entity.Id;
            }

            else
            {
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, name field was not provided or was empty"));
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
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, Provided date is invalid, please check the date"));
            }



            if (string.IsNullOrEmpty(value))
            {
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, Please provide a mandatory floating point value"));                
            }
            var ioTValue = new float();
            if (float.TryParse(value, out ioTValue))
            {
                ioTData.Value = ioTValue;
            }
            else
            {
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, Provided Value is not a valid floating point number"));                
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
        public async Task<IActionResult> DeleteIoTData(int id, string key)
        {
            if(!IsValidKey(key))
            {
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, could not auhtenticate the user with given key - The key is either invalid or disabled"));
            }
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, Please provide a valid integer Id to delete the record"));
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


        [HttpGet]
        [Route("[Controller]/writedata")]
        public async Task<IActionResult> WriteData(string name, string value, string date, string key)
        {
            #region check if the key is provided and valid             
            if (!IsValidKey(key))
            {
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, could not auhtenticate the user with given key - The key is either invalid or disabled"));
            }
            #endregion

            var ioTData = new IoTData();

            #region validate the provided data

            var existingName = string.IsNullOrEmpty(name) ? false : _context.IoTDataNames.Where(q => q.Name.ToLowerInvariant() == name.ToLowerInvariant()).Count() > 0 ? true : false;
            if (existingName)
            {
                ioTData.IoTDataNameId = _context.IoTDataNames.Where(q => q.Name.ToLowerInvariant() == name.ToLowerInvariant()).First().Id;
            }
            else if (!string.IsNullOrEmpty(name))
            {
                var newIotDataName = _context.Add(new IoTDataName()
                {
                    Name = name,
                    IoTKeyId = _context.IoTKeys.Where(q => q.Key == Guid.Parse(key)).First().Id
                });
                ioTData.IoTDataNameId = newIotDataName.Entity.Id;
            }

            else
            {
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, name field was not provided or was empty"));
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
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, Provided date is invalid, please check the date"));
            }

            if (string.IsNullOrEmpty(value))
            {
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, Please provide a mandatory floating point value"));
            }
            var ioTValue = new float();
            if (float.TryParse(value, out ioTValue))
            {
                ioTData.Value = ioTValue;
            }
            else
            {
                return new BadRequestObjectResult(new BadRequestResultModel("Operation failed, Provided Value is not a valid floating point number"));
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

        private bool IoTDataExists(int id)
        {
            return _context.IoTData.Any(e => e.Id == id);
        }

        private bool IsValidKey(string key)
        {
            return string.IsNullOrEmpty(key) ? false : (_context.IoTKeys.Where(q => q.Key.ToString().ToLowerInvariant() == key && q.Enabled ).Count() > 0)? true : false;
        }
    }
}
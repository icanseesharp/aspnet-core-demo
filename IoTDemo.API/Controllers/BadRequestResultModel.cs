using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTDemo.API.Controllers
{
    public class BadRequestResultModel
    {
        public string Message { get; set; }
        public BadRequestResultModel(string message)
        {
            Message = message;
        }
    }
}

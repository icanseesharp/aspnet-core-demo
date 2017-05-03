using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IoTDemo.API.Models
{
    public class IoTDataName
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int KeyId { get; set; }
    }
}

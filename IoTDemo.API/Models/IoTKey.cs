using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IoTDemo.API.Models
{
    public class IoTKey
    {
        [Key]
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string User { get; set; }
        public bool Enabled { get; set; }
    }
}

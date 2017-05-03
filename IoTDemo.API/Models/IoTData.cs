using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IoTDemo.API.Models
{
    public class IoTData
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int NameId { get; set; }
        public float Value { get; set; }
    }
}

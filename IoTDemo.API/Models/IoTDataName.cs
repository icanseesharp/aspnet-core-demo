using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IoTDemo.API.Models
{
    public class IoTDataName
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int IoTKeyId { get; set; }
        [ForeignKey("IoTKeyId")]
        public virtual IoTKey IoTKey { get; set; }
    }
}

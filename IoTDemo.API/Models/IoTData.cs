using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IoTDemo.API.Models
{
    public class IoTData
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty(ItemConverterType = typeof(JavaScriptDateTimeConverter))]
        public DateTime Date { get; set; }        
        public int IoTDataNameId { get; set; }
        [ForeignKey("IoTDataNameId")]
        public virtual IoTDataName IoTDataName { get; set; }
        public float Value { get; set; }
    }
}

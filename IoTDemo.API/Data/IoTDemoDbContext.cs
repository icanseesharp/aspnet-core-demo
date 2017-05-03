using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IoTDemo.API.Models
{
    public class IoTDemoDbContext : DbContext
    {
        public IoTDemoDbContext (DbContextOptions<IoTDemoDbContext> options)
            : base(options)
        {
        }

        public DbSet<IoTDemo.API.Models.IoTData> IoTData { get; set; }
    }
}

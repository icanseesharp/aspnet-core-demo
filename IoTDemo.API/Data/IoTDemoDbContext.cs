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

        public DbSet<IoTData> IoTData { get; set; }
        public DbSet<IoTDataName> IoTDataNames { get; set; }
        public DbSet<IoTKey> IoTKeys { get; set; }
    }
}

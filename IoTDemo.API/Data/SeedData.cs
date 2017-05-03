using IoTDemo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDemo.API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new IoTDemoDbContext(
             serviceProvider.GetRequiredService<DbContextOptions<IoTDemoDbContext>>()))
            {
                // Look for any movies.
                if (context.IoTData.Any())
                {
                    return;   // DB has been seeded
                }

                context.IoTData.AddRange(
                     new IoTData
                     {
                         
                     },

                     new IoTData
                     {
                         
                     },

                     new IoTData
                     {
                         
                     },

                   new IoTData
                   {
                       
                   }
                );
                context.SaveChanges();
            }
        }
    }
}

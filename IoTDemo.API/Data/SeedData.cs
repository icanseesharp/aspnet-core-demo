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
                context.Database.EnsureCreated();
                // Look for any previous data.
                if (context.IoTData.Any())
                {
                    return;   // DB has been seeded
                }
                #region Add IoTKeys                
                var key1 =context.IoTKeys.Add(new IoTKey {
                    
                    Key = Guid.NewGuid(),
                    User = "site-admin",
                    Enabled = true,
                });

                var key2 = context.IoTKeys.Add(new IoTKey
                {                    
                    Key =  Guid.NewGuid(),
                    User = "web-admin",
                    Enabled = true,
                });
                
                #endregion Add IoTKeys

                #region Add IoTNames                
                var name1 = context.IoTDataNames.Add(new IoTDataName {
                    
                    Name = "temperature",
                    IoTKeyId = key1.Entity.Id                                      
                });

                var name2 = context.IoTDataNames.Add(new IoTDataName
                {                    
                    Name = "temperature",
                    IoTKeyId = key2.Entity.Id                    
                });
                
                #endregion Add IoTNames

                #region Add IoTData                
                context.IoTData.AddRange(
                     new IoTData
                     {                         
                         Date = DateTime.Now,
                         IoTDataNameId = name1.Entity.Id,                       
                         Value = 18.5f
                     },

                     new IoTData
                     {                         
                         Date = DateTime.Now,
                         IoTDataNameId = name1.Entity.Id,                         
                         Value = 19.6f
                     },

                     new IoTData
                     {                         
                         Date = DateTime.Now,
                         IoTDataNameId = name2.Entity.Id,                         
                         Value = 3.14f
                     }
                );
                #endregion Add IoTData                

                context.SaveChanges();                
            }
        }
    }
}

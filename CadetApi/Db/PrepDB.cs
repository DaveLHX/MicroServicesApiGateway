using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadetApi.Db
{
    public static  class PrepDB
    {
        public static void MigrateAndPopulate(IApplicationBuilder app)
        {
            using (var serviceScope= app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<CadetContext>());
            }
        }
        private static void SeedData(CadetContext cadetContext)
        {
            System.Console.WriteLine("Appling Migrations...");
            cadetContext.Database.Migrate();
        }
    }
}

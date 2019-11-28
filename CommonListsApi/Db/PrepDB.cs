using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CommonListsApi.Db
{
    public static  class PrepDB
    {
        public static void MigrateAndPopulate(IApplicationBuilder app)
        {
            using (var serviceScope= app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<CommonListsContext>());
            }
        }
        private static void SeedData(CommonListsContext context)
        {
            System.Console.WriteLine("Appling Migrations...");         
            context.Database.Migrate();
            
            if (!context.Ranks.Any())
            {
                //add data
            }
        }
    }
}

using CadetApi.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CadetApi.Db
{
    public static  class PrepDB
    {
        static string[] _firstNames = new string[]{"Harry", "Ross",
                        "Bruce", "Cook",
                        "Carolyn", "Morgan",
                        "Albert", "Walker",
                        "Randy", "Reed",
                        "Larry", "Barnes",
                        "Lois", "Wilson",
                        "Jesse", "Campbell",
                        "Ernest", "Rogers",
                        "Theresa", "Patterson",
                        "Henry", "Simmons",
                        "Michelle", "Perry",
                        "Frank", "Butler",
                        "Shirley" };
        static string[] _lastNames = new string[]{"Ruth","Jackson",
                    "Debra","Allen",
                    "Gerald","Harris",
                    "Raymond","Carter",
                    "Jacqueline","Torres",
                    "Joseph","Nelson",
                    "Carlos","Sanchez",
                    "Ralph","Clark",
                    "Jean","Alexander",
                    "Stephen","Roberts",
                    "Eric","Long",
                    "Amanda","Scott",
                    "Teresa","Diaz",
                    "Wanda","Thomas" };

        public static void MigrateAndPopulate(IApplicationBuilder app)
        {
            using (var serviceScope= app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<CadetContext>());
            }
        }
       
        private static void SeedData(CadetContext context)
        {
            System.Console.WriteLine("Appling Migrations...");
           
            context.Database.Migrate();
            var rnd = new Random();
            if (!context.Cadets.Any())
            {
                for (int i = 1; i < 200; i++)
                {
                    var cdt = new Cadet()
                    {
                        FirstName = _firstNames[rnd.Next(0, _firstNames.Length)],
                        LastName = _lastNames[rnd.Next(0, _lastNames.Length)],
                        Element = rnd.Next(1, 3),
                        Program = rnd.Next(1, 5),
                        CurrentRank = rnd.Next(1, 5),
                        BirthDate = DateTime.Today.AddYears(-12 + rnd.Next(0, 4)),
                        
                    };
                    context.Add(cdt);
            }
                context.SaveChanges();               
            }
        }
    }
}

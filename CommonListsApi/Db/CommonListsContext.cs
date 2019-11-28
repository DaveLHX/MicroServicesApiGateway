using CommonListsApi.Model;
using Microsoft.EntityFrameworkCore;


namespace CommonListsApi.Db
{
    public class CommonListsContext : DbContext
    {
        public CommonListsContext(DbContextOptions<CommonListsContext> options):base(options)
        {

        }
        public DbSet<Element> Elements { get; set; }
        public DbSet<HrmsPers> HrmsPers { get; set; }
        public DbSet<Rank> Ranks { get; set; }


       
    }
}

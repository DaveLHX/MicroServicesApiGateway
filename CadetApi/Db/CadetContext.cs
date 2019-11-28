using CadetApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CadetApi.Db
{
    public class CadetContext : DbContext
    {
        public CadetContext(DbContextOptions<CadetContext> options) : base(options)
        {

        }
       
        public DbSet<Cadet> Cadets { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
          
        //    optionsBuilder.UseSqlServer(
        //        @"Server=(localdb)\mssqllocaldb;Database=Blogging;Integrated Security=True");
        //}
    }
}

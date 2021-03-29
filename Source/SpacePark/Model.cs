using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark
{
    public class BloggingContext : DbContext
    {
        public DbSet<People> People { get; set; }
        public DbSet<Spaceship> Spaceship { get; set; }

        // The following configures EF to create a Sqlite database file as `C:\blogging.db`.
        // For Mac or Linux, change this to `/tmp/blogging.db` or any other absolute path.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=localhost,1433;Database=SpaceParkDb;User Id=sa;Password=verystrong!pass123;");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestVersion.Models;

namespace DataHome.Data
{
    public class DataHomeContext : DbContext 
    {
        public DataHomeContext (DbContextOptions<DataHomeContext> options)
            : base(options)
        {

        }
        

        public DbSet<TestVersion.Models.Agent> Agent { get; set; } = default!;

        public DbSet<TestVersion.Models.Property>? Property { get; set; }

        public DbSet<TestVersion.Models.Type>? Type { get; set; }

        public DbSet<TestVersion.Models.User>? User { get; set; }


    }
}

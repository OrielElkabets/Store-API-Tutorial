using Microsoft.EntityFrameworkCore;
using StoreServices.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreServices.Data
{
    public class StoreDbContext : DbContext
    {
        public DbSet<ItemEO> Items { get; set; }
        public DbSet<UserEO> Users { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }
    }
}

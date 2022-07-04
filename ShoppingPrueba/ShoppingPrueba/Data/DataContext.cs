using Microsoft.EntityFrameworkCore;
using ShoppingPrueba.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingPrueba.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
   
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<States>().HasIndex("Name","CountryId").IsUnique();
            modelBuilder.Entity<City>().HasIndex("Name", "StateId").IsUnique();

        }
    }
}

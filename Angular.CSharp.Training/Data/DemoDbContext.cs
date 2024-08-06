using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Angular.CSharp.Training.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext() : base("Server=(localdb)\\MyDemoDB;Database=AngularCSharpDB;Trusted_Connection=True;")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DemoDbContext>());
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Department>().HasKey(d => d.Id);
        }
    }
}
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
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var newEmployees = ChangeTracker.Entries<Employee>()
                       .Where(e => e.State == EntityState.Added)
                       .ToList();

            foreach (var entry in newEmployees)
            {
                entry.Entity.Id = GenerateEmployeeId();
            }

            return base.SaveChanges();
        }

        private int GenerateEmployeeId()
        {
            var lastEmployee = Employees
                .OrderByDescending(e => e.Id)
                .FirstOrDefault();

            int newId;

            if (lastEmployee == null)
            {
                newId = 100000;
            }
            else
            {
                newId = lastEmployee.Id + 1;
            }

            return newId;
        }
    }
}
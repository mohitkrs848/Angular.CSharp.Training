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
        //public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Employee -> Department relationship
            //modelBuilder.Entity<Employee>()
            //    .HasRequired(e => e.Department)
            //    .WithMany(d => d.Employees)
            //    .HasForeignKey(e => e.EmpDepartmentID);

            //// Employee -> Manager relationship
            //modelBuilder.Entity<Employee>()
            //    .HasOptional(e => e.Manager)
            //    .WithMany(m => m.Employees)
            //    .HasForeignKey(e => e.EmpManagerID);

            //// Project -> Manager relationship
            //modelBuilder.Entity<Project>()
            //    .HasRequired(p => p.Manager)
            //    .WithMany(m => m.Projects)
            //    .HasForeignKey(p => p.ProjectManagerID);
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
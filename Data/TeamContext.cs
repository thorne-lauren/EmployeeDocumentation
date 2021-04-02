using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeDocumentation.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDocumentation.Data
{
    public class TeamContext : DbContext
    {
        public TeamContext(DbContextOptions<TeamContext> options) : base(options)
        {
        }

        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Documentation> Documentation { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supervisor>().ToTable("Supervisor");
            modelBuilder.Entity<Documentation>().ToTable("Documentation");
            modelBuilder.Entity<Employee>().ToTable("Employee");
        }
    }
}

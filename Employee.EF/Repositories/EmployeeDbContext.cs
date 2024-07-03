using Employee.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.EF.Repositories;
 public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SkillsModel>()
            .HasOne<Emp>(m => m.Employee)
            .WithMany(n => n.Skills)
            .HasForeignKey(x => x.EmployeeId);
    }

    public DbSet<Emp> Employees { get; set; }
    public DbSet<SkillsModel> Skills { get; set; }
}

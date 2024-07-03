using Employee.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.EF.Repositories;
public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeDbContext dbContext;
    public EmployeeRepository(EmployeeDbContext _dbContext)
    {
        dbContext = _dbContext;
    }
    public async Task<Emp> AddEmployees(Emp emp)
    {
        emp.Id = new Guid();
        dbContext.Employees.Add(emp);
        await dbContext.SaveChangesAsync();
        return emp;
    }

    public async Task<Emp> DeleteEmployee(Emp emp)
    {
        dbContext.Employees.Remove(emp);
        await dbContext.SaveChangesAsync();
        return emp;
    }

    public async Task<List<Emp>> GetAllEmployees()
    {
        return await dbContext.Employees.AsNoTracking().Include(m => m.Skills).ToListAsync();
    }

    public async Task<Emp?> GetEmployeeById(Guid id)
    {
        return await dbContext.Employees.AsNoTracking().Include(m => m.Skills).Where(m => m.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Emp> UpdateEmployee(Emp emp)
    {
        dbContext.Employees.Attach(emp);
        dbContext.Entry(emp).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        return emp;
    }

    public async Task<List<SkillsModel>> AddSkills(List<SkillsModel> skills)
    {
        dbContext.Skills.AddRange(skills);
        await dbContext.SaveChangesAsync();
        return skills;
    }

    public async Task<List<SkillsModel>> RemoveSkills(List<SkillsModel> skills)
    {
        dbContext.Skills.RemoveRange(skills);
        await dbContext.SaveChangesAsync();
        return skills;
    }
}

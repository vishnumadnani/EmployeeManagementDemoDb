using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Employees;
 public interface IEmployeeServices
{
    public Task<Emp> AddEmployees(Emp emp);
    public Task<Emp?> GetEmployeeById(Guid id);
    public Task<List<Emp>> GetAllEmployees();
    public Task<Emp> UpdateEmployee(Emp emp);
    public Task<Emp> DeleteEmployee(Emp emp);
    public Task<List<SkillsModel>> RemoveSkills(List<SkillsModel> skills);
}

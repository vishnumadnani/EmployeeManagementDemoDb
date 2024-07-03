using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Employees;
public class EmployeeServices : IEmployeeServices
{
    private readonly IEmployeeRepository employeeRepository;
    public EmployeeServices(IEmployeeRepository _employeeRepository)
    {
        employeeRepository = _employeeRepository;
    }
    public async Task<Emp> AddEmployees(Emp emp)
    {
        return await employeeRepository.AddEmployees(emp);
    }

    public async Task<Emp> DeleteEmployee(Emp emp)
    {
        return await employeeRepository.DeleteEmployee(emp);
    }

    public async Task<List<Emp>> GetAllEmployees()
    {
        return await employeeRepository.GetAllEmployees();
    }

    public async Task<Emp?> GetEmployeeById(Guid id)
    {
        return await employeeRepository.GetEmployeeById(id);
    }

    public async Task<Emp> UpdateEmployee(Emp emp)
    {      
        if(emp.Skills != null && emp.Skills.Count > 0)
        {
            var newSkills = emp.Skills.Where(m => m.Employee == null).ToList();
            if (emp.Skills != null && emp.Skills.Count > 0)
            {
                await employeeRepository.AddSkills(newSkills);
            }
        }
        return await employeeRepository.UpdateEmployee(emp);
    }

    public async Task<List<SkillsModel>> RemoveSkills(List<SkillsModel> skills)
    {
        return await employeeRepository.RemoveSkills(skills);
    }
}


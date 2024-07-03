using AutoMapper;
using Employee.Domain.Employees;
using Employee.EF.Repositories;
using EmployeeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using X.PagedList;

namespace EmployeeWebApp.Controllers;
public class EmployeeController : Controller
{
    private readonly IEmployeeServices employeeServices;
    private readonly IMapper mapper;
    private readonly EmployeeDbContext dbContext;
    public EmployeeController(IEmployeeServices _employeeServices, IMapper _mapper, EmployeeDbContext _dbContext)
    {
        employeeServices = _employeeServices;
        mapper = _mapper;
        dbContext = _dbContext;
    }
    public async Task<IActionResult> Index(int? page, string EmpSearch, string sortOrder)
    {
        var employeesList = await employeeServices.GetAllEmployees();
        if (!string.IsNullOrEmpty(EmpSearch))
        {
            employeesList = employeesList.Where(m => m.firstName.Contains(EmpSearch) || m.lastName.Contains(EmpSearch)).ToList();
        }
        ViewBag.FirstNameSort = string.IsNullOrEmpty(sortOrder) ? "firstName_desc" : "";
        ViewBag.EmailSort = sortOrder == "email" ? "email_desc" : "email";
        switch (sortOrder)
        {          
            case "firstName_desc":
                employeesList = employeesList.OrderByDescending(m => m.firstName).ToList();
                break;
            case "email":
                employeesList = employeesList.OrderBy(m => m.email).ToList();
                break;
            case "email_desc":
                employeesList = employeesList.OrderByDescending(m => m.email).ToList();
                break;
            default:
                employeesList = employeesList.OrderBy(m => m.firstName).ToList();
                break;
        }
        var employee = employeesList.ToPagedList(page ?? 1, 3);
        return View(employee);
    }
   
    
    public IActionResult Create()
    {      
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmpAddRequest addRequest)
    {
        if (ModelState.IsValid)
        {
            var employee = mapper.Map<Emp>(addRequest);
            if (!string.IsNullOrEmpty(addRequest.EmpSkills))
            {
                employee.Skills = new List<SkillsModel>();
                var skills = addRequest.EmpSkills.Split(" ").ToList();
                skills.RemoveAll(skill => string.IsNullOrWhiteSpace(skill));
                addRequest.EmpSkills = string.Join(" ", skills);
                foreach (var item in skills)
                {
                    SkillsModel skill = new SkillsModel();
                    skill.Id = Guid.NewGuid();
                    skill.Name = item;
                    employee.Skills.Add(skill);
                }
            }
            await employeeServices.AddEmployees(employee);
            TempData["success"] = "Employee Inserted Successfully";
            return RedirectToAction("Index");
        }
        return View(addRequest);    
    }    


    public async Task<IActionResult> Edit(Guid? id)
    {       
        if(id == null)
        {
            return NotFound();
        }
        var employee = await employeeServices.GetEmployeeById(id.Value);
        if(employee == null)
        {
            return NotFound();
        }
        var emp = mapper.Map<EmpAddRequest>(employee);
        return View(emp);     
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EmpAddRequest addRequest)
    {
        var existEmployee = await employeeServices.GetEmployeeById(addRequest.Id);
        if (existEmployee == null)
        {
            return NotFound();
        }
        mapper.Map(addRequest, existEmployee);
        List<string> skills = null;
        if (!string.IsNullOrEmpty(addRequest.EmpSkills))
        {
            skills = addRequest.EmpSkills.Split(",").ToList();
            if (skills != null && skills.Count > 0)
            {
                var deletedSkills = existEmployee.Skills.Where(m => !skills.Contains(m.Name)).ToList();
                if (deletedSkills != null && deletedSkills.Count > 0)
                {
                    await employeeServices.RemoveSkills(deletedSkills);
                }
            }
            foreach (var skillName in skills)
            {
                if (!existEmployee.Skills.Any(n => n.Name == skillName))
                {
                    existEmployee.Skills.Add(new SkillsModel
                    {
                        Id = Guid.NewGuid(),
                        Name = skillName,
                        EmployeeId = existEmployee.Id
                    });
                }
            }
        }
        await employeeServices.UpdateEmployee(existEmployee);
        TempData["success"] = "Employee Updated Successfully";
        return RedirectToAction("Index");
    }


    [HttpGet]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteData(Guid? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var emp = await employeeServices.GetEmployeeById(id.Value);
        if (emp == null)
        {
            return NotFound();
        }
        await employeeServices.DeleteEmployee(emp);
        TempData["success"] = "Employee Deleted Successfully";
        return RedirectToAction("Index");
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Employees;
public class Emp
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "FirstName is mandatory")]
    public string? firstName { get; set; }

    [Required(ErrorMessage = "LastName is mandatory")]
    public string? lastName { get; set; }

    [EmailAddress(ErrorMessage = "Type valid Email")]
    public string? email { get; set; }

    [Required(ErrorMessage = "Gender is mandatory")]
    public string? gender { get; set; }

    [Required(ErrorMessage = "Select the Value")]
    public string? designation { get; set; }
    public bool Learning { get; set; }
    public bool Cricket { get; set; }
    public bool Music { get; set; }
    public string? ImageView { get; set; }
    [Required(ErrorMessage = "Give Birth Date")]
    public DateTime birthDate { get; set; }
    public virtual List<SkillsModel>? Skills { get; set; }
}


public class SkillsModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid EmployeeId { get; set; }
    public virtual Emp? Employee { get; set; }
}

public enum designation
{
    Trainee,
    JrDeveloper,
    SrDeveloper,
    ProjectManager
}

namespace EmployeeWebApp.Models;

public class EmpAddRequest
{
    public Guid Id { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? email { get; set; }
    public string? gender { get; set; }
    public string? designation { get; set; }
    public bool Learning { get; set; }
    public bool Cricket { get; set; }
    public bool Music { get; set; }
    public string? ImageView { get; set; }
    public DateTime birthDate { get; set; }
    public string? EmpSkills { get; set; }
}


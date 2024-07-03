using Employee.Domain.Employees;
using Employee.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebApp;

public class Startup
{
    public IConfiguration configuration;
    private readonly IWebHostEnvironment env;
    public Startup(IConfiguration _configuration, IWebHostEnvironment _env)
    {
        configuration = _configuration;
        env = _env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddDbContext<EmployeeDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTransient<IEmployeeRepository , EmployeeRepository>();
        services.AddTransient<IEmployeeServices, EmployeeServices>();
    }

    public void Configure(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}

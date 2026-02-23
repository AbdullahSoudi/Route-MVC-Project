using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Route.BLL.Mapping;
using Route.BLL.Services.Classes;
using Route.BLL.Services.EmailSender;
using Route.BLL.Services.Interfaces;
using Route.DAL.Data.Contexts;
using Route.DAL.Models;
using Route.DAL.Repositories.Classes;
using Route.DAL.Repositories.Interfaces;
using Route.PL.Mapping;

namespace Route.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Configure Services
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddScoped<IEmailSender, EmailSender>();

            builder.Services.AddAutoMapper(e => e.AddProfile<MappingProfile>());
            builder.Services.AddAutoMapper(e => e.AddProfile<PresentationMappingProfile>());

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
            {
                op.Password.RequireDigit = false;
                op.Password.RequireUppercase = false;
                op.Password.RequireLowercase = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequiredLength = 3;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

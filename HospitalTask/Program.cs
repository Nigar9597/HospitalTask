using HospitalTask.DAL;
using HospitalTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HospitalTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
           
            
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>
                (opt =>
                {
                    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequiredLength = 10;
                    opt.SignIn.RequireConfirmedEmail = true;
                    opt.Lockout.MaxFailedAccessAttempts = 5;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(5);
                }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
                
                

            var app = builder.Build();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );
            


            app.MapControllerRoute(


                name: "Default",
                pattern: "{Controller=Home}/{Action=Index}/{id?}");

            app.Run();
        }
    }
}

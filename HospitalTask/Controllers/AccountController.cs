using HospitalTask.DAL;
using HospitalTask.Models;
using HospitalTask.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HospitalTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(AppDbContext appDbContext,
            UserManager<AppUser> userManager)
        {
            _context = appDbContext;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserVM createUserVM)
        {
            if(!ModelState.IsValid)
            {
                return View(createUserVM);
            }
           
            AppUser appUser=new AppUser();
            appUser.FirstName = createUserVM.FirstName;
            appUser.LastName = createUserVM.LastName;
            appUser.Email = createUserVM.Email;
            appUser.UserName = createUserVM.Username;
         
        var result=  await  _userManager.CreateAsync(appUser,createUserVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(createUserVM);
                }
            }
            return RedirectToAction(nameof(Index), "Home");
        }

    }
}

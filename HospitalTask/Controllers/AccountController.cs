using HospitalTask.DAL;
using HospitalTask.Models;
using HospitalTask.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Register(CreateUserVM createUserVM)
        {
            if(!ModelState.IsValid)
            {
                return View(createUserVM);
            }
            return View();
        }

    }
}

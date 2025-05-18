using HospitalTask.DAL;
using HospitalTask.Models;
using HospitalTask.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Tasks;

namespace HospitalTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(AppDbContext appDbContext,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _context = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
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
        [HttpGet]
        public IActionResult Login ()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserVM loginUserVM)
        {
            
            if (!ModelState.IsValid)
            {

                return View(loginUserVM);
          
            }
            AppUser user = await _userManager.FindByNameAsync(loginUserVM.UsernameorEmailAddress);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginUserVM.UsernameorEmailAddress);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Username or email is wrong");
                    return View(loginUserVM);
                }
            }
           var result= await _signInManager.PasswordSignInAsync(user, loginUserVM.Password,loginUserVM.IsPersistent, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Password is wrong");
                return View();
            
            }
            
            return RedirectToAction(nameof(Index), "Home");

           
            
        }

        public async Task<IActionResult> Logout()
        {
         await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        
        
        }

    }
}

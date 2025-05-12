using Microsoft.AspNetCore.Mvc;

namespace HospitalTask.Areas.Admin
{
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

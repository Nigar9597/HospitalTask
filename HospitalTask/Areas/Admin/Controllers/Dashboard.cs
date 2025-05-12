using HospitalTask.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalTask.Areas.Admin.Controllers;

[Area ("Admin")]
public class Dashboard : Controller
{
    private readonly AppDbContext _db;
    public Dashboard(AppDbContext appDbContext)
    {
        _db = appDbContext;
    }

    public IActionResult Index()
    {

        var doctor3s=_db.Doctor3s.Include(d=>d.Department3).ToList();
        return View(doctor3s);
    }
}

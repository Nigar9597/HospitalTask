using HospitalTask.DAL;
using HospitalTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace HospitalTask.Controllers;

public class HomeController : Controller
{

    private readonly AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        List<Doctor3>Doctor3s= _context.Doctor3s.Include(d => d.Department3).ToList();
        return View(Doctor3s);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Service()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }
}

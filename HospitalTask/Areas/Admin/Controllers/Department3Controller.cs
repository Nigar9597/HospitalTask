using HospitalTask.Areas.ViewModels.Department3VMs;
using HospitalTask.DAL;
using HospitalTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalTask.Areas.Admin.Controllers;

[Area("Admin")]
public class Department3Controller : Controller
{
    private readonly AppDbContext _db;
    public Department3Controller(AppDbContext appDbContext)
    {
        _db = appDbContext;
    }
    public IActionResult Index()
    {
        var department3s = _db.Department3s.Include(d => d.Doctor3s).ToList();
        return View(department3s);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]

    public IActionResult Create(Department3 department3)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();

        }

        _db.Department3s.Add(department3);
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));

    }

    public IActionResult Delete(int id)

    {
        Department3? department3 = _db.Department3s.Find(id);
        if (department3 == null) { return NotFound("Department could not found"); }
        _db.Department3s.Remove(department3);
        _db.SaveChanges();
        return RedirectToAction("Index");

    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        Department3 existingDepartment3 = _db.Department3s.Find(id);
        if (existingDepartment3 == null) { return NotFound("Department could not found"); }
        return View(nameof(Update), existingDepartment3);

    }
    [HttpPost]
    public IActionResult Update(Department3 department3)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Create), department3);
        }
       Department3? existingDepartment3 = _db.Department3s.FirstOrDefault(d => d.Id == department3.Id);
        if (existingDepartment3 == null) { return NotFound("Department could not be found"); }

        existingDepartment3.Title = department3.Title;
        existingDepartment3.Description = department3.Description;
        //_db.Department3s.Update(department3);
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult AssignDoctor3s(int id)
    {
        Department3 existingDepartment3 = _db.Department3s.Find(id);
        if (existingDepartment3 == null) { return NotFound("Department could not found"); }
        BatchAssignVM batchAssignVM = new BatchAssignVM()
        {
                Department3Id= id,
                AllDoctor3s = _db.Doctor3s.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToList(), 
                Doctor3Ids=new List<int>()
        };
            return View(batchAssignVM);
        
    
    }
    [HttpPost]
   public IActionResult AssignDoctor3s(BatchAssignVM model)
     {  var doctor3s = _db.Doctor3s.Where(d => model.Doctor3Ids.Contains(d.Id));
        
        foreach(var doctor3 in doctor3s)
        {
            doctor3.Department3Id=model.Department3Id;
        };
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
        
    }

}

using HospitalTask.DAL;
using HospitalTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalTask.Areas.Admin.Controllers;

[Area("Admin")]

public class Doctor3Controller : Controller
{

    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public Doctor3Controller(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
    {
        _db = appDbContext;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index()
    {
        var doctor3s = _db.Doctor3s.Include(d => d.Department3).ToList();
        return View(doctor3s);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]

    public IActionResult Create(Doctor3 doctor3)
    {
        if (!ModelState.IsValid)
        {
            return View(doctor3);

        }
       

        if (!doctor3.ImageUpload.ContentType.Contains("image"))
        {
            ModelState.AddModelError("ImageUpload", "File must be Image format");
            return View(doctor3);

        }
        string filename = Guid.NewGuid()+ doctor3.ImageUpload.FileName;
        string path = _webHostEnvironment.WebRootPath + @"\assets\UploadImages\Doctor3s\";
        using (FileStream fileStream = new FileStream(path + filename, FileMode.Create))

        {
            doctor3.ImageUpload.CopyTo(fileStream);
        }

        doctor3.ImgUrl= filename;
        _db.Doctor3s.Add(doctor3);
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));

    }

    public IActionResult Delete(int id)
    {
        Doctor3? doctor3 = _db.Doctor3s.Find(id);
        if (doctor3 == null) { return NotFound("Doctor could not be found"); }
        _db.Doctor3s.Remove(doctor3);
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]

    public IActionResult Update(int Id)
    {
        Doctor3 existingDoctor3 = _db.Doctor3s.Find(Id);
        if (existingDoctor3 == null) { return NotFound("Doctor could not found"); }
        return View(nameof(Update), existingDoctor3);
    }

    [HttpPost]
    public IActionResult Update(Doctor3 doctor3)
    {
        /*if (!ModelState.IsValid)
        {
            return View(nameof(Create), doctor3);
        }*/
        Doctor3? existingDoctor3 = _db.Doctor3s.FirstOrDefault(v => v.Id == doctor3.Id);
        if (existingDoctor3 == null) { return NotFound("Doctor could not be found"); }
       /* _db.Doctor3s.Update(doctor3);*/
        existingDoctor3.Name = doctor3.Name;
        existingDoctor3.Department3Id = doctor3.Department3Id;
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));

    }
}

using Microsoft.AspNetCore.Mvc;
using StarbucksWeb.Models;
using Microsoft.AspNetCore.Http;

namespace StarbucksWeb.Controllers;

public class MenuController : Controller
{
    private readonly DataContext _context;

    public MenuController(DataContext context)
    {
        _context = context;
    }

    // 1. LİSTELEME VE FİLTRELEME (cat parametresi eklendi)
    public IActionResult Index(string cat)
    {
        // Önce tüm listeyi alalım
        var sorgu = _context.Icecekler.AsQueryable();

        // Eğer sol menüden bir kategoriye tıklandıysa (cat boş değilse) filtrele
        if (!string.IsNullOrEmpty(cat))
        {
            sorgu = sorgu.Where(x => x.Kategori == cat);
        }

        var liste = sorgu.ToList();
        return View(liste);
    }

    // 2. EKLEME (GET) - Admin Kontrolü
    public IActionResult Ekle()
    {
        if (HttpContext.Session.GetString("Rol") != "Admin") return RedirectToAction("Login", "Account");
        return View();
    }

    // 3. EKLEME (POST) - Admin Kontrolü
    [HttpPost]
    public IActionResult Ekle(Icecek yeni)
    {
        if (HttpContext.Session.GetString("Rol") != "Admin") return RedirectToAction("Login", "Account");

        if (!ModelState.IsValid) return View(yeni);

        _context.Icecekler.Add(yeni);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // 4. DÜZENLEME (GET) - Admin Kontrolü
    public IActionResult Edit(int id)
    {
        if (HttpContext.Session.GetString("Rol") != "Admin") return RedirectToAction("Login", "Account");

        var icecek = _context.Icecekler.Find(id);
        if (icecek == null) return NotFound();
        return View(icecek);
    }

    // 5. DÜZENLEME (POST) - Admin Kontrolü
    [HttpPost]
    public IActionResult Edit(Icecek guncel)
    {
        if (HttpContext.Session.GetString("Rol") != "Admin") return RedirectToAction("Login", "Account");

        if (!ModelState.IsValid) return View(guncel);

        _context.Icecekler.Update(guncel);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // 6. SİLME - Admin Kontrolü
    public IActionResult Sil(int id)
    {
        if (HttpContext.Session.GetString("Rol") != "Admin") return RedirectToAction("Login", "Account");

        var icecek = _context.Icecekler.Find(id);
        if (icecek != null)
        {
            _context.Icecekler.Remove(icecek);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
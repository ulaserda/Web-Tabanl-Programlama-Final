using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Session işlemleri için şart

namespace StarbucksWeb.Controllers;

public class AccountController : Controller
{
    // Giriş Sayfasını Açar (GET)
    public IActionResult Login()
    {
        return View();
    }

    // Giriş Butonuna Basınca Çalışır (POST)
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Basit bir admin kontrolü
        if (username == "admin" && password == "1234")
        {
            // Sisteme "Bu adam Admin" etiketi yapıştırıyoruz
            HttpContext.Session.SetString("Rol", "Admin");
            return RedirectToAction("Index", "Menu");
        }

        // Hatalı giriş yapılırsa
        ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
        return View();
    }

    // Çıkış Yapma
    public IActionResult Logout()
    {
        HttpContext.Session.Clear(); // Oturumu temizle
        return RedirectToAction("Index", "Home");
    }
}
using Microsoft.AspNetCore.Mvc;
using StarbucksWeb.Models; // Bu satır 'DataContext'i bulmasını sağlar
using System.Text.Json;

namespace StarbucksWeb.Controllers;

public class CartController : Controller
{
    private readonly DataContext _context;

    public CartController(DataContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var sepetJson = HttpContext.Session.GetString("Sepet");
        List<CartItem> sepet = string.IsNullOrEmpty(sepetJson) 
            ? new List<CartItem>() 
            : JsonSerializer.Deserialize<List<CartItem>>(sepetJson) ?? new List<CartItem>();

        return View(sepet);
    }

    // GÜNCELLENEN AKILLI EKLEME METODU
    public IActionResult Ekle(int id, string returnUrl, string cat)
    {
        var urun = _context.Icecekler.Find(id);
        if (urun == null) return RedirectToAction("Index", "Menu");

        var sepetJson = HttpContext.Session.GetString("Sepet");
        List<CartItem> sepet = string.IsNullOrEmpty(sepetJson) 
            ? new List<CartItem>() 
            : JsonSerializer.Deserialize<List<CartItem>>(sepetJson) ?? new List<CartItem>();

        var mevcutItem = sepet.FirstOrDefault(x => x.IcecekId == id);
        if (mevcutItem != null)
        {
            mevcutItem.Adet++;
        }
        else
        {
            sepet.Add(new CartItem { IcecekId = id, Ad = urun.Ad, Fiyat = urun.Fiyat, Adet = 1 });
        }

        HttpContext.Session.SetString("Sepet", JsonSerializer.Serialize(sepet));

        // YÖNLENDİRME MANTIĞI:
        // 1. Eğer sepet sayfasındaysak oraya dön
        if (returnUrl == "Cart") 
        {
            return RedirectToAction("Index");
        }

        // 2. Eğer bir kategoriden gelindiyse o kategoriye geri dön
        if (!string.IsNullOrEmpty(cat))
        {
            return RedirectToAction("Index", "Menu", new { cat = cat });
        }

        // 3. Varsayılan olarak ana menüye dön
        return RedirectToAction("Index", "Menu");
    }

    public IActionResult Azalt(int id)
    {
        var sepetJson = HttpContext.Session.GetString("Sepet");
        if (string.IsNullOrEmpty(sepetJson)) return RedirectToAction("Index");

        var sepet = JsonSerializer.Deserialize<List<CartItem>>(sepetJson) ?? new List<CartItem>();
        var urun = sepet.FirstOrDefault(x => x.IcecekId == id);

        if (urun != null)
        {
            if (urun.Adet > 1) urun.Adet--;
            else sepet.Remove(urun);
        }

        HttpContext.Session.SetString("Sepet", JsonSerializer.Serialize(sepet));
        return RedirectToAction("Index");
    }

    public IActionResult Sil(int id)
    {
        var sepetJson = HttpContext.Session.GetString("Sepet");
        if (string.IsNullOrEmpty(sepetJson)) return RedirectToAction("Index");

        var sepet = JsonSerializer.Deserialize<List<CartItem>>(sepetJson) ?? new List<CartItem>();
        var urun = sepet.FirstOrDefault(x => x.IcecekId == id);

        if (urun != null) sepet.Remove(urun);

        HttpContext.Session.SetString("Sepet", JsonSerializer.Serialize(sepet));
        return RedirectToAction("Index");
    }

    public IActionResult Temizle()
    {
        HttpContext.Session.Remove("Sepet");
        return RedirectToAction("Index");
    }
    public IActionResult Onay()
{
    // Sipariş verildiği için sepeti session'dan siliyoruz
    HttpContext.Session.Remove("Sepet");
    return View();
}
}
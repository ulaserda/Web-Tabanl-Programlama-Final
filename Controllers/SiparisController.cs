using Microsoft.AspNetCore.Mvc;
using StarbucksWeb.Models;

public class SiparisController : Controller
{
    // Form sayfasını açar
    public IActionResult Odeme()
    {
        return View();
    }

    // Form gönderildiğinde çalışır
    [HttpPost]
    public IActionResult Tamamla(Siparis model)
    {
        // Burada normalde bankaya istek gönderilir ama biz "Başarılı" diyelim
        ViewBag.Mesaj = "Siparişiniz başarıyla alındı! Afiyet olsun.";
        return View("Basarili");
    }
}
namespace StarbucksWeb.Models;

public class Siparis
{
    public int Id { get; set; }
    public string AdSoyad { get; set; } = "";
    public string Adres { get; set; } = "";
    public string Telefon { get; set; } = "";
    
    // Kart Bilgileri (Veritabanına kaydetmesek bile formda alacağız)
    public string KartNo { get; set; } = "";
    public string SKT { get; set; } = "";
    public string CVV { get; set; } = "";

    public decimal ToplamTutar { get; set; }
    public DateTime SiparisTarihi { get; set; } = DateTime.Now;
}
namespace StarbucksWeb.Models;

public class CartItem
{
    public int IcecekId { get; set; }
    public string Ad { get; set; } = string.Empty;
    public decimal Fiyat { get; set; }
    public int Adet { get; set; }
}
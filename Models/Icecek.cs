using System.ComponentModel.DataAnnotations;

namespace StarbucksWeb.Models;

public class Icecek
{
    public int Id { get; set; }

    [Required(ErrorMessage = "İçecek adı zorunludur.")]
    public string Ad { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lütfen boyut seçiniz.")]
    public string Boyut { get; set; } = string.Empty;

    // --- İŞTE BURAYA EKLİYORUZ ---
    [Required(ErrorMessage = "Lütfen bir kategori seçiniz.")]
    public string Kategori { get; set; } = string.Empty; 
    // ----------------------------

    [Required(ErrorMessage = "Fiyat girmelisiniz.")]
    [Range(1, 2000, ErrorMessage = "Geçerli bir fiyat giriniz.")]
    public decimal Fiyat { get; set; }
}
using Microsoft.EntityFrameworkCore;

namespace StarbucksWeb.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Icecek> Icecekler { get; set; }
}
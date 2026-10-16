using Microsoft.EntityFrameworkCore;
using Pop_Riana_Lab2.Models;

public class Pop_Riana_Lab2Context(DbContextOptions<Pop_Riana_Lab2Context> options) : DbContext(options)
{
    public DbSet<Pop_Riana_Lab2.Models.Book> Book { get; set; } = default!;

    public DbSet<Pop_Riana_Lab2.Models.Publisher> Publisher { get; set; } = default!;

    public DbSet<Pop_Riana_Lab2.Models.Author> Author { get; set; } = default!;


}

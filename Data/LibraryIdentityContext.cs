using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pop_Riana_Lab2.Data;

public class LibraryIdentityContext : IdentityDbContext<ApplicationUser>
{
    public LibraryIdentityContext(DbContextOptions<LibraryIdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

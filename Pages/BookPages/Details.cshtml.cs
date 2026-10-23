using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pop_Riana_Lab2.Models;

namespace Pop_Riana_Lab2.Pages.BookPages;

public class DetailsModel : PageModel
{
    private readonly Pop_Riana_Lab2Context _context;

    public DetailsModel(Pop_Riana_Lab2Context context)
    {
        _context = context;
    }

    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

        if (book == null)
        {
            return NotFound();
        }

        Book = book;
        return Page();
    }
}
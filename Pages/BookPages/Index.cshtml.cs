using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pop_Riana_Lab2.Models;

namespace Pop_Riana_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Pop_Riana_Lab2Context _context;

    public IndexModel(Pop_Riana_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    public SelectList? Authors { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public async Task OnGetAsync()
    {
        var books = _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .AsQueryable();

        if (AuthorID.HasValue)
        {
            books = books.Where(b => b.AuthorID == AuthorID);
        }

        var authorList = await _context.Author
            .OrderBy(a => a.LastName)
            .ToListAsync();
        Authors = new SelectList(authorList, "ID", "FullName");

        Book = await books.ToListAsync();
    }
}
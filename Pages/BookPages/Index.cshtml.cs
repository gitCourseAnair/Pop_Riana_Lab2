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
    public BookData BookD { get; set; } = default!;
    public int BookID { get; set; }
    public int CategoryID { get; set; }


    public string TitleSort { get; set; }
    public string AuthorSort { get; set; }
    public string CurrentFilter { get; set; }


    public SelectList? Authors { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
    {
        BookD = new BookData();


        TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";
        CurrentFilter = searchString;


        var books = _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Include(b => b.BookCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .AsQueryable();


        if (AuthorID.HasValue)
        {
            books = books.Where(b => b.AuthorID == AuthorID);
        }

        var authorList = await _context.Author
            .OrderBy(a => a.LastName)
            .ToListAsync();
        Authors = new SelectList(authorList, "ID", "FullName");

        BookD.Books = await books
            .OrderBy(b => b.Title)
            .ToListAsync();


        if (!String.IsNullOrEmpty(searchString))
        {
            BookD.Books = BookD.Books.Where(s => s.Author.FirstName.Contains(searchString)
                                              || s.Author.LastName.Contains(searchString)
                                              || s.Title.Contains(searchString));
        }


        if (id != null)
        {
            BookID = id.Value;
            Book? selected = BookD.Books.FirstOrDefault(i => i.ID == id.Value);
            if (selected != null)
            {
                BookD.Categories = selected.BookCategories.Select(s => s.Category);
            }
        }


        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Title);
                break;
            case "author_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Author.FullName);
                break;
            case "author":
                BookD.Books = BookD.Books.OrderBy(s => s.Author.FullName);
                break;
            default:
                BookD.Books = BookD.Books.OrderBy(s => s.Title);
                break;
        }

        Book = BookD.Books.ToList();
    }
}


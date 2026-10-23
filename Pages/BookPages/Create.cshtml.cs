using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pop_Riana_Lab2.Models;


namespace Pop_Riana_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Pop_Riana_Lab2Context _context;

    public CreateModel(Pop_Riana_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public IActionResult OnGet()
    {
        LoadDropdowns();

       
        var book = new Book();
        book.BookCategories = new List<BookCategory>();
        PopulateAssignedCategoryData(_context, book);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        // construim lista de categorii alese
        var newBook = new Book();
        newBook.BookCategories = new List<BookCategory>();

        if (selectedCategories != null)
        {
            foreach (var cat in selectedCategories)
            {
                newBook.BookCategories.Add(new BookCategory
                {
                    CategoryID = int.Parse(cat)
                });
            }
        }

        if (!ModelState.IsValid)
        {
            LoadDropdowns();
          
            PopulateAssignedCategoryData(_context, newBook);
            return Page();
        }

        Book.BookCategories = newBook.BookCategories;
        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private void LoadDropdowns()
    {
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
        ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName");
    }
}
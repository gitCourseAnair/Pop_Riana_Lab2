using Pop_Riana_Lab2.Models;

namespace Pop_Riana_Lab2.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; } = default!;
        public IEnumerable<Book> Books { get; set; } = default!;
    }
}
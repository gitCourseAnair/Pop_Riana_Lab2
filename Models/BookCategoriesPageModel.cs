
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Pop_Riana_Lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList = new();

        public void PopulateAssignedCategoryData(
            Pop_Riana_Lab2Context context,
            Book book)
        {
            var allCategories = context.Category;
            var bookCategories = new HashSet<int>(
                book.BookCategories.Select(c => c.CategoryID));

            foreach (var category in allCategories)
            {
                AssignedCategoryDataList.Add(
                    new AssignedCategoryData
                    {
                        CategoryID = category.ID,
                        Name = category.CategoryName,
                        Assigned = bookCategories.Contains(category.ID)
                    });
            }
        }

        public void UpdateBookCategories(
            Pop_Riana_Lab2Context context,
            string[] selectedCategories,
            Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>(
                bookToUpdate.BookCategories.Select(c => c.CategoryID));

            foreach (var category in context.Category)
            {
                if (selectedCategoriesHS.Contains(category.ID.ToString()))
                {
                    if (!bookCategories.Contains(category.ID))
                    {
                        bookToUpdate.BookCategories.Add(
                            new BookCategory
                            {
                                BookID = bookToUpdate.ID,
                                CategoryID = category.ID
                            });
                    }
                }
                else
                {
                    if (bookCategories.Contains(category.ID))
                    {
                        var categoryToRemove = bookToUpdate.BookCategories
                            .SingleOrDefault(c => c.CategoryID == category.ID);

                        if (categoryToRemove != null)
                        {
                            bookToUpdate.BookCategories.Remove(categoryToRemove);
                        }
                    }
                }
            }
        }
    }
}

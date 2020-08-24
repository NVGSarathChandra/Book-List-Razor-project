using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooklistRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BooklistRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly BookDbContext bookDbContext;
        public IndexModel(BookDbContext dbContext)
        {
            this.bookDbContext = dbContext;
        }
        public IEnumerable<Book> books;
        public async Task OnGet()                   //OnGet mthod Similar to page_load in Asp.Net
        {
            books = await bookDbContext.Book.ToListAsync();
        }
        public async Task<IActionResult> OnPostDelete(int bookId)
        {
            var bookFromDb = await bookDbContext.Book.FindAsync(bookId);
            if (bookFromDb == null)
            {
                return NotFound("Unable to delete, Book not found");
            }
            bookDbContext.Book.Remove(bookFromDb);
            await bookDbContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}

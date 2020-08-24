using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooklistRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooklistRazor.Pages.BookList
{
    public class EditBookListModel : PageModel
    {
        private readonly BookDbContext bookDbContext;

        [BindProperty]
        public Book book { get; set; }
        public EditBookListModel(BookDbContext dbContext)
        {
            this.bookDbContext = dbContext;
        }
        public async Task OnGet(int bookId)
        {
            book = await bookDbContext.Book.FindAsync(bookId);
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var bookFromDb = await bookDbContext.Book.FindAsync(book.BookId);
                bookFromDb.BookName = book.BookName;
                bookFromDb.AuthorName = book.AuthorName;
                bookFromDb.ISBN = book.ISBN;
                await bookDbContext.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            else
            {
                return Page();
            }
        }

    }
}


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
    public class UpsertModel : PageModel
    {
        private readonly BookDbContext bookDbContext;

        [BindProperty]
        public Book book { get; set; }
        public UpsertModel(BookDbContext dbContext)
        {
            this.bookDbContext = dbContext;
        }
        public async Task<IActionResult> OnGet(int? bookId)
        {
            book = new Book();
            if (bookId == null)
            {
                return Page();
            }
            book = await bookDbContext.Book.FirstOrDefaultAsync(u => u.BookId == bookId);
            if (book == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (book.BookId == 0)
                {
                    book.ISBN = book.ISBN + book.BookId.ToString();
                   await bookDbContext.Book.AddAsync(book);
                }
                else
                {
                    var bookFromDb = await bookDbContext.Book.FindAsync(book.BookId);
                    bookFromDb.BookName = book.BookName;
                    bookFromDb.AuthorName = book.AuthorName;
                    bookFromDb.ISBN = book.ISBN;
                    /*Can also update as following
                    bookDbContext.Book.Update(book)*/;
                }
                
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

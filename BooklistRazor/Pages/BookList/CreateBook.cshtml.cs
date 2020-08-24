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
    public class CreateBookModel : PageModel
    {
        private readonly BookDbContext bookDbContext;
        [BindProperty]
        public Book book { get; set; }
        public CreateBookModel(BookDbContext dbContextOptions)
        {
            this.bookDbContext = dbContextOptions;
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid) {
                await bookDbContext.Book.AddAsync(book);
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

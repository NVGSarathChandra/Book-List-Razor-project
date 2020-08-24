using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooklistRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooklistRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly BookDbContext dbContext;
        public BookController(BookDbContext bookDbContext)
        {
            this.dbContext = bookDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(new { data = await dbContext.Book.ToListAsync() });

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int bookId)
        {
            var bookFromDb = await dbContext.Book.FirstOrDefaultAsync(e => e.BookId == bookId);
            if (bookFromDb == null)
            {
                return NotFound("Unable to delete, Book not found");
            }
            dbContext.Book.Remove(bookFromDb);
            await dbContext.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successfull" });
        }
    }
}

using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooks(int Id)
        {
            return await _bookRepository.Get(Id);
        }

        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            var newBook = await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook.Id }, newBook);

        }

        [HttpPut]
        public async Task<ActionResult> PutBooks (int Id, [FromBody] Book book)
        {
            if (book.Id != Id)
                return BadRequest();

            await _bookRepository.Update(book);
            return NoContent(); 
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var deleteBook = await _bookRepository.Get(Id);
            
            if (deleteBook == null)
                return NotFound();

            await _bookRepository.Delete(deleteBook.Id);
            return NoContent();
        }
        
    }
}

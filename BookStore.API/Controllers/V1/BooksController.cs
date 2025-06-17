using Asp.Versioning;
using BookStore.API.Endpoints;
using BookStore.Application.Dtos.Books;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.V1;

[ApiController]
[ApiVersion(1.0)]
[Authorize]
public class BooksController(IBookService bookService) : ControllerBase
{
    /// <summary>
    /// Retrieves all books from the database.
    /// </summary>
    /// <returns>List of books.</returns>
    [HttpGet(ApiEndpoints.V1.Books.GetAll, Name = "GetBooks")]
    public async Task<IActionResult> GetBooksAsync(CancellationToken token)
    {
        var books = await bookService.GetAllAsync(token);
        return Ok(books);
    }

    /// <summary>
    /// Retrieves a specific book by its unique identifier.
    /// </summary>
    /// <returns>The requested book if found; otherwise NotFound.</returns>
    [HttpGet(ApiEndpoints.V1.Books.GetById, Name = "GetBook")]
    public async Task<IActionResult> GetBookByIdAsync(Guid id, CancellationToken token)
    {
        var book = await bookService.GetByIdAsync(id, token);
        return Ok(book);
    }

    /// <summary>
    /// Creates a new book entry in the database.
    /// </summary>
    /// <returns>The created book with its location.</returns>
    [HttpPost(ApiEndpoints.V1.Books.Create, Name = "CreateBook")]
    public async Task<IActionResult> CreateBookAsync([FromBody] BookCreateDto createDto, CancellationToken token)
    {
        var createdBook = await bookService.CreateAsync(createDto, token);
        return CreatedAtRoute("GetBook", new { id = createdBook.Id }, createdBook);
    }

    /// <summary>
    /// Updates the details of an existing book.
    /// </summary>
    /// <returns>NoContent if updated successfully; otherwise NotFound.</returns>
    [HttpPut(ApiEndpoints.V1.Books.Update, Name = "UpdateBook")]
    public async Task<IActionResult> UpdateBookAsync(Guid id, [FromBody] BookUpdateDto updateDto, CancellationToken token)
    {
        var result = await bookService.UpdateAsync(id, updateDto, token);
        return result ? NoContent() : NotFound();
    }

    /// <summary>
    /// Deletes a book from the database.
    /// </summary>
    /// <returns>NoContent if deleted; otherwise NotFound.</returns>
    [HttpDelete(ApiEndpoints.V1.Books.Delete, Name = "DeleteBook")]
    public async Task<IActionResult> DeleteBookAsync(Guid id, CancellationToken token)
    {
        var result = await bookService.DeleteAsync(id, token);
        return result ? NoContent() : NotFound();
    }
}

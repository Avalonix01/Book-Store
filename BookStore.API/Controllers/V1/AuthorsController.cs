using Asp.Versioning;
using BookStore.API.Endpoints;
using BookStore.Application.Dtos.Authors;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.V1;

[ApiController]
[ApiVersion(1.0)]
[Authorize]
public class AuthorsController(IAuthorService authorService) : ControllerBase
{
    /// <summary>
    /// Returns a list of all authors stored in the database.
    /// </summary>
    /// <returns>A list of authors.</returns>
    [HttpGet(ApiEndpoints.V1.Authors.GetAll, Name = "GetAuthors")]
    public async Task<IActionResult> GetAuthorsAsync(CancellationToken token)
    {
        var authors = await authorService.GetAllAsync(token);
        return Ok(authors);
    }

    /// <summary> Returns an author by their unique identifier.</summary>
    /// <returns>The author if found; otherwise, 404 Not Found.</returns>
    [HttpGet(ApiEndpoints.V1.Authors.GetById, Name = "GetAuthorById")]
    public async Task<IActionResult> GetAuthorByIdAsync(Guid id, CancellationToken token)
    {
        var author = await authorService.GetByIdAsync(id, token);
        return author is null ? NotFound() : Ok(author);
    }

    /// <summary>
    /// Creates a new author.
    /// </summary>
    /// <returns>The created author with a 201 Created response.</returns>
    [HttpPost(ApiEndpoints.V1.Authors.Create, Name = "CreateAuthor")]
    public async Task<IActionResult> AddAuthorAsync([FromBody] AuthorCreateDto createDto, CancellationToken token)
    {
        var createdAuthor = await authorService.CreateAsync(createDto, token);
        return CreatedAtRoute("GetAuthorById", new { id = createdAuthor.Id }, createdAuthor);
    }

    /// <summary>
    /// Updates an existing author by their unique identifier.
    /// </summary>
    /// <returns>204 No Content if updated; 404 Not Found if the author does not exist.</returns>
    [HttpPut(ApiEndpoints.V1.Authors.Update, Name = "UpdateAuthor")]
    public async Task<IActionResult> UpdateAuthorAsync(Guid id, [FromBody] AuthorUpdateDto updateDto, CancellationToken token)
    {
        var result = await authorService.UpdateAsync(id, updateDto, token);
        return result ? NoContent() : NotFound();
    }

    /// <summary>
    /// Deletes an author by their unique identifier.
    /// </summary>
    /// <returns>204 No Content if deleted; 404 Not Found if the author does not exist.</returns>
    [HttpDelete(ApiEndpoints.V1.Authors.Delete, Name = "DeleteAuthor")]
    public async Task<IActionResult> DeleteAuthorAsync(Guid id, CancellationToken token)
    {
        var result = await authorService.DeleteAsync(id, token);
        return result ? NoContent() : NotFound();
    }
}
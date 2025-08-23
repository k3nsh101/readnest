using ReadNest.Repositories;
using ReadNest.Entities;
using ReadNest.Dtos;
using ReadNest.Mapping;
using ReadNest.Utils;

namespace ReadNest.Endpoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this IEndpointRouteBuilder app)
    {
        var bookGroup = app.MapGroup("books");

        bookGroup.MapGet("/", async (IBookRepository repo) =>
        {
            var books = await repo.GetAllBooks();

            return Results.Ok(books.Select(BookMapping.ToBookSummaryDto));
        });

        bookGroup.MapGet("/{id}", async (Guid id, IBookRepository repo) =>
        {
            var book = await repo.GetBookById(id);
            return book is not null ? Results.Ok(book.ToBookDetailsDto()) : Results.NotFound();
        }).WithName("GetBookById");

        bookGroup.MapPost("/cover", async (HttpRequest request, IWebHostEnvironment env) =>
        {
            var form = await request.ReadFormAsync();
            var file = form.Files.GetFile("coverUrl");

            if (file == null || file.Length == 0)
                return Results.BadRequest("No file uploaded");

            var relativeUrl = await BookUtils.SaveBookCover(file, env);
            return Results.Ok(new { url = relativeUrl });
        });

        bookGroup.MapPost("/", async (CreateBookDto newBook, IBookRepository repo) =>
        {
            Book book = newBook.ToEntity();
            var createdBook = await repo.AddBook(book);
            BookDetailsDto bookDto = createdBook.ToBookDetailsDto();

            return Results.CreatedAtRoute("GetBookById", new { id = bookDto.BookId }, bookDto);
        });

        bookGroup.MapPost("/{id}/complete", async (Guid id, IBookRepository repo) =>
        {
            var success = await repo.MarkRead(id);

            return success ? Results.NoContent() : Results.NotFound();
        });

        bookGroup.MapPut("/{id}", async (Guid id, UpdateBookDto updatedBook, IBookRepository repo) =>
        {
            var success = await repo.UpdateBook(id, updatedBook);
            return success ? Results.NoContent() : Results.NotFound();
        });

        bookGroup.MapPut("/{id}/cover", async (Guid id, HttpRequest request, IWebHostEnvironment env, IBookRepository repo) =>
        {
            var form = await request.ReadFormAsync();
            var file = form.Files.GetFile("coverUrl");

            if (file == null || file.Length == 0)
                return Results.BadRequest("No file uploaded");

            var relativeUrl = await BookUtils.SaveBookCover(file, env);

            var success = await repo.UpdateCoverUrl(id, relativeUrl);

            return success ? Results.NoContent() : Results.NotFound();
        });

        bookGroup.MapDelete("/{id}", async (Guid id, IBookRepository repo) =>
        {
            var success = await repo.DeleteBook(id);
            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}

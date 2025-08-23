using ReadNest.Repositories;
using ReadNest.Entities;
using ReadNest.Dtos;
using ReadNest.Mapping;
using ReadNest.Utils;

namespace ReadNest.Endpoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapGet("/books", async (IBookRepository repo) =>
        {
            var books = await repo.GetAllBooks();

            return Results.Ok(books.Select(BookMapping.ToBooksDto));
        });

        app.MapGet("/books/{id}", async (int id, IBookRepository repo) =>
        {
            var book = await repo.GetBookById(id);
            return book is not null ? Results.Ok(book.ToBookDto()) : Results.NotFound();
        });

        app.MapPost("/books/cover", async (HttpRequest request, IWebHostEnvironment env) =>
        {
            var form = await request.ReadFormAsync();
            var file = form.Files.GetFile("coverUrl");

            if (file == null || file.Length == 0)
                return Results.BadRequest("No file uploaded");

            var relativeUrl = await BookUtils.SaveBookCover(file, env);
            return Results.Ok(new { url = relativeUrl });
        });

        app.MapPost("/books", async (CreateBookDto newBook, IBookRepository repo) =>
        {
            Book book = newBook.ToEntity();
            var createdBook = await repo.AddBook(book);
            BookDto bookDto = createdBook.ToBookDto();

            return Results.Created($"/books/{createdBook.BookId}", bookDto);
        });

        app.MapPost("/books/{id}/complete", async (int id, IBookRepository repo) =>
        {
            var success = await repo.MarkRead(id);

            return success ? Results.NoContent() : Results.NotFound();
        });

        app.MapPut("/books", async (UpdateBookDto changedBook, IBookRepository repo) =>
        {
            Book book = changedBook.ToUpdateEntity();

            var updatedBook = await repo.UpdateBook(changedBook.BookId, book);
            return updatedBook is not null ? Results.NoContent() : Results.NotFound();
        });

        app.MapPut("/books/{id}/cover", async (int id, HttpRequest request, IWebHostEnvironment env, IBookRepository repo) =>
        {
            var form = await request.ReadFormAsync();
            var file = form.Files.GetFile("coverUrl");

            if (file == null || file.Length == 0)
                return Results.BadRequest("No file uploaded");

            var relativeUrl = await BookUtils.SaveBookCover(file, env);

            var success = await repo.UpdateCoverUrl(id, relativeUrl);

            return success ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/books/{id}", async (int id, IBookRepository repo) =>
        {
            var success = await repo.DeleteBook(id);
            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}

using ReadNest.Repositories;
using ReadNest.Entities;
using ReadNest.Dtos;
using ReadNest.Mapping;

namespace ReadNest.Endpoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapGet("/books", async (IBookRepository repo) =>
        {
            var books = await repo.GetAllBooks();

            return Results.Ok(books.Select(BookMapping.ToDto));
        });

        app.MapGet("/books/{id}", async (int id, IBookRepository repo) =>
        {
            var book = await repo.GetBookById(id);
            return book is not null ? Results.Ok(book.ToDto()) : Results.NotFound();
        });

        app.MapPost("/books/cover", async (HttpRequest request, IWebHostEnvironment env) =>
        {
            var form = await request.ReadFormAsync();
            var file = form.Files.GetFile("coverUrl");

            if (file == null || file.Length == 0)
                return Results.BadRequest("No file uploaded");

            var uploadsDir = Path.Combine(env.WebRootPath ?? "wwwroot", "book-covers");
            Directory.CreateDirectory(uploadsDir);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativeUrl = fileName;
            return Results.Ok(new { url = relativeUrl });
        });

        app.MapPost("/books", async (CreateBookDto newBook, IBookRepository repo) =>
        {
            Book book = newBook.ToEntity();
            var createdBook = await repo.AddBook(book);
            BookDto bookDto = createdBook.ToDto();

            return Results.Created($"/books/{createdBook.BookId}", bookDto);
        });

        app.MapPut("/books", async (UpdateBookDto changedBook, IBookRepository repo) =>
        {
            Book book = changedBook.ToUpdateEntity();

            var updatedBook = await repo.UpdateBook(changedBook.BookId, book);
            return updatedBook is not null ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/books/{id}", async (int id, IBookRepository repo) =>
        {
            var success = await repo.DeleteBook(id);
            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}

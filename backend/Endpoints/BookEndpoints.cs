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

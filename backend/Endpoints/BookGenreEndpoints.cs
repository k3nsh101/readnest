using ReadNest.Repositories;
using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Endpoints;

public static class BookGenreEndpoints
{
    public static void MapBookGenreEndpoints(this WebApplication app)
    {
        app.MapGet("/genres", async (IBookGenreRepository repo) =>
        {
            var users = await repo.GetAllBookGenres();
            return Results.Ok(users);
        });

        app.MapPost("/genres", async (CreateBookGenreDto newGenre, IBookGenreRepository repo) =>
        {
            BookGenre genre = new()
            {
                Name = newGenre.Name
            };

            var createdGenre = await repo.AddBookGenre(genre);
            return Results.Created($"/genres/{createdGenre.GenreId}", createdGenre);
        });

        app.MapPut("/genres", async (UpdateBookGenreDto changedGenre, IBookGenreRepository repo) =>
        {
            BookGenre genre = new()
            {
                GenreId = changedGenre.GenreId,
                Name = changedGenre.Name
            };

            var updatedGenre = await repo.UpdateBookGenre(genre);
            return updatedGenre is not null ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/genres/{id}", async (int id, IBookGenreRepository repo) =>
        {
            var success = await repo.DeleteBookGenre(id);
            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}

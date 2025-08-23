using ReadNest.Repositories;
using ReadNest.Entities;
using ReadNest.Dtos;
using ReadNest.Mapping;

namespace ReadNest.Endpoints;

public static class BookGenreEndpoints
{
    public static void MapBookGenreEndpoints(this IEndpointRouteBuilder app)
    {
        var genreGroup = app.MapGroup("genres");

        genreGroup.MapGet("/", async (IBookGenreRepository repo) =>
        {
            var genres = await repo.GetAllBookGenres();
            return Results.Ok(genres);
        });

        genreGroup.MapPost("/", async (CreateBookGenreDto newGenre, IBookGenreRepository repo) =>
        {
            BookGenre genre = newGenre.ToEntity();
            var createdGenre = await repo.AddBookGenre(genre);
            return Results.Created($"/genres/{createdGenre.GenreId}", createdGenre);
        });

        genreGroup.MapDelete("/{id}", async (Guid id, IBookGenreRepository repo) =>
        {
            var success = await repo.DeleteBookGenre(id);
            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}

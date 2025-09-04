using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using ReadNest.Dtos;
using ReadNest.Entities;
using ReadNest.Data;

namespace ReadNest.Tests.Integration.Endpoint;

public class BookGenreEndpointTests
{
    [Fact]
    public async Task AddBookGenre_ReturnCreated()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var genre = new CreateBookGenreDto("Fantasy");
        var response = await client.PostAsJsonAsync("/genres", genre);
        var result = await response.Content.ReadFromJsonAsync<BookGenreDto>();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal("Fantasy", result.Name);
    }

    [Fact]
    public async Task GetAllBookGenres_ReturnSuccessAndEmptyList_WhenEmpty()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var response = await client.GetAsync("/genres");
        var result = await response.Content.ReadFromJsonAsync<List<BookGenreDto>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllBookGenres_ReturnSuccessAndAllGenres()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var genre1 = new BookGenre
        {
            GenreId = Guid.NewGuid(),
            Name = "Sci-Fi"
        };

        var genre2 = new BookGenre
        {
            GenreId = Guid.NewGuid(),
            Name = "Sci-Fi"
        };

        db.BookGenres.Add(genre1);
        db.BookGenres.Add(genre2);
        db.SaveChanges();

        var response = await client.GetAsync("/genres");
        var result = await response.Content.ReadFromJsonAsync<List<BookGenreDto>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Collection(result,
            g =>
            {
                Assert.Equal(genre1.GenreId, g.GenreId);
                Assert.Equal(genre1.Name, g.Name);
            },
            g =>
            {
                Assert.Equal(genre2.GenreId, g.GenreId);
                Assert.Equal(genre2.Name, g.Name);
            });
    }

    [Fact]
    public async Task DeleteBookGenre_ShouldReturnNoContent_WhenExists()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var genre = new BookGenre
        {
            GenreId = Guid.NewGuid(),
            Name = "Sci-Fi"
        };

        db.BookGenres.Add(genre);
        db.SaveChanges();

        var response = await client.DeleteAsync($"/genres/{genre.GenreId}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteBookGenre_ShouldReturnNotFound_WhenGenreNotFound()
    {
        using var factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var response = await client.DeleteAsync($"/genres/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}

using Microsoft.EntityFrameworkCore;
using ReadNest.Data;
using ReadNest.Entities;
using ReadNest.Repositories;

public class BookGenreRepositoryTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task AddBookGenre_ShouldSaveGenre()
    {
        var context = GetInMemoryDbContext();
        var repo = new BookGenreRepository(context);
        var genreId = Guid.NewGuid();
        var genreName = "Fantasy";

        var newGenre = new BookGenre
        {
            GenreId = genreId,
            Name = genreName
        };

        var result = await repo.AddBookGenre(newGenre);
        var savedGenre = await context.BookGenres.FirstOrDefaultAsync();

        Assert.NotNull(savedGenre);
        Assert.Equal(genreId, savedGenre.GenreId);
        Assert.Equal(genreName, savedGenre.Name);
        Assert.Equal(newGenre, result);
    }

    [Fact]
    public async Task GetAllBookGenres_ShouldReturnAllGenres()
    {
        var context = GetInMemoryDbContext();
        var genre1 = new BookGenre
        {
            GenreId = Guid.NewGuid(),
            Name = "Sci-Fi"
        };

        var genre2 = new BookGenre
        {
            GenreId = Guid.NewGuid(),
            Name = "Mystery"
        };

        context.BookGenres.AddRange(genre1, genre2);
        await context.SaveChangesAsync();

        var repo = new BookGenreRepository(context);

        var genres = await repo.GetAllBookGenres();

        Assert.Equal(2, genres.Count);
        Assert.Collection(genres,
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
    public async Task DeleteBookGenre_ShouldRemoveGenre_WhenExists()
    {
        var context = GetInMemoryDbContext();
        var genre = new BookGenre
        {
            GenreId = Guid.NewGuid(),
            Name = "Thriller"
        };

        context.BookGenres.Add(genre);
        await context.SaveChangesAsync();

        var repo = new BookGenreRepository(context);

        var result = await repo.DeleteBookGenre(genre.GenreId);
        var allGenres = await context.BookGenres.ToListAsync();

        Assert.True(result);
        Assert.Empty(allGenres);
    }

    [Fact]
    public async Task DeleteBookGenre_ShouldReturnFalse_WhenGenreNotFound()
    {
        var context = GetInMemoryDbContext();
        var repo = new BookGenreRepository(context);

        var result = await repo.DeleteBookGenre(Guid.NewGuid());

        Assert.False(result);
    }
}

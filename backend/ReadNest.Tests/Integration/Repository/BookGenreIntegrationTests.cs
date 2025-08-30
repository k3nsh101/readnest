using Microsoft.EntityFrameworkCore;
using ReadNest.Data;
using ReadNest.Repositories;
using ReadNest.Entities;

public class BookGenreRepositoryTests
{
    AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddBookGenre_SaveGenreToDB()
    {
        var context = GetInMemoryDbContext();
        var repo = new BookGenreRepository(context);

        var genreId = Guid.NewGuid();
        var name = "Test Genre 1";

        var newGenre = new BookGenre
        {
            GenreId = genreId,
            Name = name
        };

        await repo.AddBookGenre(newGenre);
        var createdGenre = await context.BookGenres.FirstOrDefaultAsync();

        Assert.NotNull(createdGenre);
        Assert.Equal(genreId, createdGenre.GenreId);
        Assert.Equal(name, createdGenre.Name);
    }

    [Fact]
    public async Task GetAllGenres()
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
            Name = "Fantasy"
        };

        context.BookGenres.AddRange([genre1, genre2]);
        context.SaveChanges();

        var repo = new BookGenreRepository(context);

        await repo.GetAllBookGenres();
        var genres = await context.BookGenres.ToListAsync();

        Assert.Collection(genres,
            b =>
            {
                Assert.Equal(genre1.GenreId, b.GenreId);
                Assert.Equal(genre1.Name, b.Name);
            },
            b =>
            {
                Assert.Equal(genre2.GenreId, b.GenreId);
                Assert.Equal(genre2.Name, b.Name);
            }
        );
    }

    [Fact]
    public async Task DeleteGenre()
    {
        var context = GetInMemoryDbContext();

        var genre = new BookGenre
        {
            GenreId = Guid.NewGuid(),
            Name = "Sci-Fi"
        };

        context.BookGenres.Add(genre);
        context.SaveChanges();

        var repo = new BookGenreRepository(context);

        await repo.DeleteBookGenre(genre.GenreId);
        var genres = await context.BookGenres.ToListAsync();

        Assert.Empty(genres);
    }
}

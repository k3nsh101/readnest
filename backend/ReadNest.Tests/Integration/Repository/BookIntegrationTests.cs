using Microsoft.EntityFrameworkCore;
using ReadNest.Entities;
using ReadNest.Dtos;
using ReadNest.Repositories;

namespace ReadNest.Tests.Integration.Repo;

public class BookRepositoryTests
{
    [Fact]
    public async Task AddBook_ShouldSaveBookWithRequiredProperties()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();

        var genre = new BookGenre
        {
            GenreId = Guid.NewGuid(),
            Name = "Fantasy"
        };

        context.BookGenres.Add(genre);
        await context.SaveChangesAsync();

        var repo = new BookRepository(context);
        var book = new Book
        {
            BookId = Guid.NewGuid(),
            Title = "My Book",
            Author = "Author A",
            TotalPages = 200,
            GenreId = genre.GenreId
        };

        var result = await repo.AddBook(book);
        var savedBook = await context.Books.Include(b => b.Genre).FirstOrDefaultAsync();

        Assert.NotNull(savedBook);
        Assert.Equal(book.BookId, savedBook.BookId);
        Assert.Equal("My Book", savedBook.Title);
        Assert.Equal("Author A", savedBook.Author);
        Assert.Equal(200, savedBook.TotalPages);
        Assert.Equal(ReadStatus.NotStarted, savedBook.Status); // default
        Assert.Equal(0, savedBook.PagesRead);             // default
        Assert.True(savedBook.Owned);                     // default
        Assert.Equal(genre.GenreId, savedBook.GenreId);
        Assert.NotNull(savedBook.Genre);
        Assert.Equal("Fantasy", savedBook.Genre?.Name);
    }

    [Fact]
    public async Task GetAllBooks_ShouldIncludeGenre()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();
        var genre = new BookGenre { GenreId = Guid.NewGuid(), Name = "Sci-Fi" };
        context.BookGenres.Add(genre);

        var book1 = new Book
        {
            BookId = Guid.NewGuid(),
            Title = "Book 1",
            Author = "A",
            TotalPages = 100,
            GenreId = genre.GenreId
        };

        var book2 = new Book
        {
            BookId = Guid.NewGuid(),
            Title = "Book 2",
            Author = "B",
            TotalPages = 150,
            GenreId = genre.GenreId
        };

        context.Books.AddRange(book1, book2);
        await context.SaveChangesAsync();

        var repo = new BookRepository(context);
        var books = await repo.GetAllBooks();

        Assert.Collection(books,
            b =>
            {
                Assert.Equal(book1.BookId, b.BookId);
                Assert.Equal("Book 1", b.Title);
                Assert.NotNull(b.Genre);
                Assert.Equal("Sci-Fi", b.Genre?.Name);
            },
            b =>
            {
                Assert.Equal(book2.BookId, b.BookId);
                Assert.Equal("Book 2", b.Title);
                Assert.NotNull(b.Genre);
                Assert.Equal("Sci-Fi", b.Genre?.Name);
            });
    }

    [Fact]
    public async Task GetBookById_ShouldReturnBookWithGenre()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();
        var genre = new BookGenre
        {
            GenreId = Guid.NewGuid(),
            Name = "Mystery"
        };

        var book = new Book
        {
            BookId = Guid.NewGuid(),
            Title = "Detective Story",
            Author = "Author X",
            TotalPages = 120,
            GenreId = genre.GenreId
        };

        context.BookGenres.Add(genre);
        context.Books.Add(book);
        await context.SaveChangesAsync();

        var repo = new BookRepository(context);
        var result = await repo.GetBookById(book.BookId);

        Assert.NotNull(result);
        Assert.Equal(book.BookId, result.BookId);
        Assert.Equal("Detective Story", result.Title);
        Assert.NotNull(result.Genre);
        Assert.Equal("Mystery", result.Genre?.Name);
    }

    [Fact]
    public async Task UpdateBook_ShouldModifyAllFields()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();
        var genre = new BookGenre
        {
            GenreId = Guid.NewGuid(),
            Name = "Fantasy"
        };

        var book = new Book
        {
            BookId = Guid.NewGuid(),
            Title = "Old Title",
            Author = "Old Author",
            TotalPages = 100,
            GenreId = genre.GenreId
        };

        context.BookGenres.Add(genre);
        context.Books.Add(book);
        await context.SaveChangesAsync();

        var repo = new BookRepository(context);
        var dto = new UpdateBookDto(
            "New Title",
            "New Author",
            200,
            50,
            ReadStatus.Reading,
            5,
            "Updated",
            genre.GenreId,
            false
        );

        var result = await repo.UpdateBook(book.BookId, dto);
        var updated = await context.Books.FirstAsync();

        Assert.True(result);
        Assert.Equal("New Title", updated.Title);
        Assert.Equal("New Author", updated.Author);
        Assert.Equal(200, updated.TotalPages);
        Assert.Equal(50, updated.PagesRead);
        Assert.Equal(ReadStatus.Reading, updated.Status);
        Assert.Equal(5, updated.Rating);
        Assert.Equal("Updated", updated.Remarks);
        Assert.True(updated.Owned);
        Assert.Equal(genre.GenreId, updated.GenreId);
    }

    [Fact]
    public async Task MarkRead_ShouldSetStatusCompletedAndPagesRead()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();

        var book = new Book
        {
            BookId = Guid.NewGuid(),
            Title = "Book",
            Author = "Author",
            GenreId = Guid.NewGuid(),
            TotalPages = 150,
            PagesRead = 30,
            Status = ReadStatus.Reading
        };

        context.Books.Add(book);
        await context.SaveChangesAsync();

        var repo = new BookRepository(context);
        var result = await repo.MarkRead(book.BookId);
        var updated = await context.Books.FirstAsync();

        Assert.True(result);
        Assert.Equal(ReadStatus.Completed, updated.Status);
        Assert.Equal(150, updated.PagesRead);
    }

    [Fact]
    public async Task UpdateCoverUrl_ShouldSetCover()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();
        var book = new Book
        {
            BookId = Guid.NewGuid(),
            Title = "Book",
            Author = "Author",
            GenreId = Guid.NewGuid(),
            TotalPages = 100
        };

        context.Books.Add(book);
        await context.SaveChangesAsync();

        var repo = new BookRepository(context);
        var url = "http://example.com/cover.jpg";
        var result = await repo.UpdateCoverUrl(book.BookId, url);
        var updated = await context.Books.FirstAsync();

        Assert.True(result);
        Assert.Equal(url, updated.CoverUrl);
    }

    [Fact]
    public async Task DeleteBook_ShouldRemoveBook()
    {
        var context = CustomAppDbContext.GetInMemoryDbContext();
        var book = new Book
        {
            BookId = Guid.NewGuid(),
            Title = "Book",
            Author = "Author",
            GenreId = Guid.NewGuid(),
            TotalPages = 100
        };

        context.Books.Add(book);
        await context.SaveChangesAsync();

        var repo = new BookRepository(context);
        var result = await repo.DeleteBook(book.BookId);
        var allBooks = await context.Books.ToListAsync();

        Assert.True(result);
        Assert.Empty(allBooks);
    }
}

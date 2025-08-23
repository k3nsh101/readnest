using Microsoft.EntityFrameworkCore;
using ReadNest.Data;
using ReadNest.Entities;

namespace ReadNest.Repositories;

public class BookRepository : IBookRepository
{
    readonly AppDbContext _appDbContext;

    public BookRepository(AppDbContext context)
    {
        _appDbContext = context;
    }

    public async Task<List<Book>> GetAllBooks()
    {
        return await _appDbContext.Books
            .Include(b => b.Genre)
            .ToListAsync();
    }

    public async Task<Book?> GetBookById(int id)
    {
        var book = await _appDbContext.Books
            .Include(b => b.Genre)
            .FirstOrDefaultAsync(b => b.BookId == id);

        if (book == null)
        {
            return null;
        }

        return book;
    }

    public async Task<Book> AddBook(Book newBook)
    {
        _appDbContext.Books.Add(newBook);
        await _appDbContext.SaveChangesAsync();

        return await _appDbContext.Books
              .Include(b => b.Genre)
              .FirstAsync(b => b.BookId == newBook.BookId);
    }

    public async Task<Book?> UpdateBook(int id, Book updatedBook)
    {
        if (id != updatedBook.BookId)
        {
            return null;
        }

        var book = await _appDbContext.Books.FindAsync(id);

        if (book == null)
        {
            return null;
        }

        book.Name = updatedBook.Name;
        book.Author = updatedBook.Author;
        book.TotalPages = updatedBook.TotalPages;
        book.PagesRead = updatedBook.PagesRead;
        book.Status = updatedBook.Status;
        book.Rating = updatedBook.Rating;
        book.Remarks = updatedBook.Remarks;
        book.GenreId = updatedBook.GenreId;
        book.Owned = updatedBook.Owned;
        book.CoverUrl = updatedBook.CoverUrl;

        await _appDbContext.SaveChangesAsync();
        return book;
    }

    public async Task<bool> MarkRead(int id)
    {
        var book = await _appDbContext.Books.FindAsync(id);
        if (book == null)
        {
            return false;
        }

        book.Status = ReadStatus.Completed;
        book.PagesRead = book.TotalPages;
        await _appDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteBook(int id)
    {
        var book = await _appDbContext.Books.FindAsync(id);

        if (book == null)
        {
            return false;
        }

        _appDbContext.Books.Remove(book);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}

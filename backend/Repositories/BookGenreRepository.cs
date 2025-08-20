using Microsoft.EntityFrameworkCore;
using ReadNest.Data;
using ReadNest.Entities;

namespace ReadNest.Repositories;

public class BookGenreRepository : IBookGenreRepository
{
    readonly AppDbContext _appDbContext;

    public BookGenreRepository(AppDbContext context)
    {
        _appDbContext = context;
    }

    public async Task<List<BookGenre>> GetAllBookGenres()
    {
        return await _appDbContext.BookGenres.ToListAsync();
    }

    public async Task<BookGenre> AddBookGenre(BookGenre newBookGenre)
    {
        _appDbContext.BookGenres.Add(newBookGenre);
        await _appDbContext.SaveChangesAsync();
        return newBookGenre;
    }

    public async Task<BookGenre?> UpdateBookGenre(BookGenre updatedBookGenre)
    {
        var bookGenre = await _appDbContext.BookGenres.FindAsync(updatedBookGenre.GenreId);

        if (bookGenre == null)
        {
            return null;
        }

        bookGenre.Name = updatedBookGenre.Name;

        await _appDbContext.SaveChangesAsync();
        return bookGenre;
    }

    public async Task<bool> DeleteBookGenre(int id)
    {
        var bookGenre = await _appDbContext.BookGenres.FindAsync(id);

        if (bookGenre == null)
        {
            return false;
        }

        _appDbContext.BookGenres.Remove(bookGenre);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}

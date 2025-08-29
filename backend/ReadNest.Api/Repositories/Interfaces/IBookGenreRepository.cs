using ReadNest.Entities;

namespace ReadNest.Repositories;

public interface IBookGenreRepository
{
    Task<List<BookGenre>> GetAllBookGenres();
    Task<BookGenre> AddBookGenre(BookGenre bookGenre);
    Task<bool> DeleteBookGenre(Guid id);
}

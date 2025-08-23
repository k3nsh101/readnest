using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllBooks();
    Task<Book?> GetBookById(Guid id);
    Task<Book> AddBook(Book book);
    Task<bool> UpdateBook(Guid id, UpdateBookDto updatedBook);
    Task<bool> UpdateCoverUrl(Guid id, string url);
    Task<bool> MarkRead(Guid id);
    Task<bool> DeleteBook(Guid id);
}

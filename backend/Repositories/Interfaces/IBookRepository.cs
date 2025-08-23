using ReadNest.Entities;

namespace ReadNest.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllBooks();
    Task<Book?> GetBookById(int id);
    Task<Book> AddBook(Book book);
    Task<Book?> UpdateBook(int id, Book updateBook);
    Task<bool> MarkRead(int id);
    Task<bool> DeleteBook(int id);
}

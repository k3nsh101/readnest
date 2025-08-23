using ReadNest.Dtos;
using ReadNest.Entities;

namespace ReadNest.Mapping;

public static class BookMapping
{
    public static Book ToEntity(this CreateBookDto book)
    {
        return new Book()
        {
            Name = book.Name,
            Author = book.Author,
            TotalPages = book.TotalPages,
            GenreId = book.GenreId,
            Status = ReadStatus.Unread,
            Owned = !book.Borrowed,
            CoverUrl = book.CoverUrl
        };
    }

    public static Book ToUpdateEntity(this UpdateBookDto book)
    {
        return new Book()
        {
            BookId = book.BookId,
            Name = book.Name,
            Author = book.Author,
            TotalPages = book.TotalPages,
            PagesRead = book.PagesRead,
            Status = book.Status,
            Rating = book.Rating,
            Remarks = book.Remarks,
            GenreId = book.GenreId,
            Owned = !book.Borrowed,
        };
    }

    public static BooksDto ToBooksDto(this Book book)
    {
        return new(
            book.BookId,
            book.Name,
            book.Status,
            book.Genre!.ToDto(),
            book.Owned,
            $"/book-covers/{book.CoverUrl}"
        );
    }

    public static BookDto ToBookDto(this Book book)
    {
        return new(
            book.BookId,
            book.Name,
            book.Author,
            book.TotalPages,
            book.PagesRead,
            book.Rating,
            book.Status,
            book.Remarks,
            book.Genre!.ToDto(),
            book.Owned,
            $"/book-covers/{book.CoverUrl}"
        );
    }

}

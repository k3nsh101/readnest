using ReadNest.Dtos;
using ReadNest.Entities;

namespace ReadNest.Mapping;

public static class BookMapping
{
    public static Book ToEntity(this CreateBookDto book)
    {
        return new Book()
        {
            BookId = Guid.NewGuid(),
            Title = book.Title,
            Author = book.Author,
            TotalPages = book.TotalPages,
            GenreId = book.GenreId,
            Status = ReadStatus.NotStarted,
            Owned = !book.Borrowed,
            CoverUrl = book.CoverUrl
        };
    }

    public static BookSummaryDto ToBookSummaryDto(this Book book)
    {
        var coverUrl = book.CoverUrl is not null ? $"/book-covers/{book.CoverUrl}" : null;

        return new(
            book.BookId,
            book.Title,
            book.Status,
            book.Genre!.ToDto(),
            book.Owned,
            coverUrl
        );
    }

    public static BookDetailsDto ToBookDetailsDto(this Book book)
    {
        var coverUrl = book.CoverUrl is not null ? $"/book-covers/{book.CoverUrl}" : null;
        var genre = new BookGenreDto(book.Genre!.GenreId, book.Genre!.Name);

        return new(
            book.BookId,
            book.Title,
            book.Author,
            book.TotalPages,
            book.PagesRead,
            book.Rating,
            book.Status,
            book.Remarks,
            genre,
            book.Owned,
            coverUrl
        );
    }

}

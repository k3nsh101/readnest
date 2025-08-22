namespace ReadNest.Dtos;

public record BooksDto(
    int BookId,
    string Name,
    ReadStatus Status,
    BookGenreDto Genre,
    bool Owned,
    string? CoverUrl
);

namespace ReadNest.Dtos;

public record BookDto(
    int BookId,
    string Name,
    string Author,
    int TotalPages,
    int PagesRead,
    int? Rating,
    ReadStatus Status,
    string? Remarks,
    BookGenreDto Genre,
    bool Owned,
    string? CoverUrl
);

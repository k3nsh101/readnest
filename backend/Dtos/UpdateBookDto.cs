namespace ReadNest.Dtos;

public record UpdateBookDto(
    int BookId,
    string Name,
    string Author,
    int TotalPages,
    int PagesRead,
    ReadStatus Status,
    int? Rating,
    string? Remarks,
    int GenreId,
    bool Borrowed,
    string? CoverUrl
);

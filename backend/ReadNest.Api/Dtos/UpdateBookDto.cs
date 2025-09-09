namespace ReadNest.Dtos;

public record UpdateBookDto(
    string Title,
    string Author,
    int TotalPages,
    int PagesRead,
    ReadStatus Status,
    int? Rating,
    string? Remarks,
    Guid GenreId
);

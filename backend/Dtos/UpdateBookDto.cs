namespace ReadNest.Dtos;

public record UpdateBookDto(
    int BookId,
    string Name,
    string Author,
    int TotalPages,
    ReadStatus Status,
    int? Rating,
    string? Remarks,
    int GenreId
);

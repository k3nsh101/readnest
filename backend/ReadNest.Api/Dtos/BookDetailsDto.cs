namespace ReadNest.Dtos;

public record BookDetailsDto(
    Guid BookId,
    string Title,
    string Author,
    int TotalPages,
    int PagesRead,
    int? Rating,
    ReadStatus Status,
    string? Remarks,
    string Genre,
    bool Owned,
    string? CoverUrl
);

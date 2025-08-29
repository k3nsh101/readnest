namespace ReadNest.Dtos;

public record CreateBookDto(
    string Title,
    string Author,
    int TotalPages,
    Guid GenreId,
    bool Borrowed,
    string? CoverUrl
);

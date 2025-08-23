namespace ReadNest.Dtos;

public record CreateBookDto(
    string Name,
    string Author,
    int TotalPages,
    int GenreId,
    bool Borrowed,
    string? CoverUrl
);

namespace ReadNest.Dtos;

public record UpdateBookGenreDto(
    Guid GenreId,
    string Name
);

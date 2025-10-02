namespace ReadNest.Dtos;

public record CreateReadingLogDto(
    DateOnly Date,
    Guid BookId,
    int CurrentPage
);

namespace ReadNest.Dtos;

public record DeleteReadingLogDto(
    DateOnly Date,
    Guid BookId
);


namespace ReadNest.Dtos;

public record UpdateReadingLogDto(
    DateOnly Date,
    Guid BookId,
    int PagesRead
);

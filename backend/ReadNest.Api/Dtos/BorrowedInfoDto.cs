namespace ReadNest.Dtos;

public record BorrowedInfoDto(
    Guid Id,
    string BookName,
    string LenderName,
    DateOnly BorrowedDate,
    DateOnly DueDate,
    DateOnly? ReturnedDate
);

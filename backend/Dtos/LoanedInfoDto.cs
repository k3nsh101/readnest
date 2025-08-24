namespace ReadNest.Dtos;

public record LoanedInfoDto(
    Guid Id,
    string BookName,
    string BorrowerName,
    DateOnly LoanedDate,
    DateOnly DueDate,
    DateOnly? ReturnedDate
);

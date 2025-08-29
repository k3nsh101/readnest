namespace ReadNest.Dtos;

public record CreateLoanedInfoDto(
    Guid Id,
    Guid BookId,
    string BorrowerName,
    DateOnly LoanedDate,
    DateOnly DueDate
);

namespace ReadNest.Entities;

public class LoanedInfo
{
    public required Guid Id { get; set; }
    public required Guid BookId { get; set; }
    public required string BorrowerName { get; set; }
    public required DateOnly LoarnedDate { get; set; }
    public required DateOnly DueDate { get; set; }
    public DateOnly? ReturnedDate { get; set; }

    public Book? Book { get; set; }
}

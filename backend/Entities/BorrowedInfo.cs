namespace ReadNest.Entities;

public class BorrowedInfo
{
    public Guid Id { get; set; }
    public required Guid BookId { get; set; }
    public required string LenderName { get; set; }
    public required DateOnly BorrowedDate { get; set; }
    public required DateOnly DueDate { get; set; }
    public DateOnly? ReturnedDate { get; set; }

    public Book? Book { get; set; }
}

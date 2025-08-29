namespace ReadNest.Entities;

public class ReadingLog
{
    public required DateOnly Date { get; set; }
    public required Guid BookId { get; set; }
    public required int PagesRead { get; set; }

    public Book? Book { get; set; }
}

namespace ReadNest.Entities;

public class Book
{
    public int BookId { get; set; }
    public required string Name { get; set; }
    public required int OwnerId { get; set; }
    public BookGenre? Genre { get; set; }
    public required string Author { get; set; }
    public required int TotalPages { get; set; }
    public ReadStatus Status { get; set; }
    public int Rating { get; set; }
    public string? Remarks { get; set; }

    public required User Owner { get; set; }
}

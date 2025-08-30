namespace ReadNest.Entities;

public class Book
{
    public required Guid BookId { get; set; }
    public required string Title { get; set; }
    public required Guid GenreId { get; set; }
    public required string Author { get; set; }
    public required int TotalPages { get; set; }
    public ReadStatus Status { get; set; } = ReadStatus.NotStarted;
    public int PagesRead { get; set; } = 0;
    public int? Rating { get; set; }
    public string? Remarks { get; set; }
    public bool Owned { get; set; } = true;
    public string? CoverUrl { get; set; }

    public BookGenre? Genre { get; set; }
}

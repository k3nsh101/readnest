using System.ComponentModel.DataAnnotations;

namespace ReadNest.Entities;

public class BookGenre
{
    [Key]
    public required Guid GenreId { get; set; }
    public required string Name { get; set; }
}

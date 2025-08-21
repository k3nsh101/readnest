namespace ReadNest.Entities;

public class User
{
    public int UserId { get; set; }
    public required string Name { get; set; }
    // public ICollection<Book>? Books { get; set; }
}

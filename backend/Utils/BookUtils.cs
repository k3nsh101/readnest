namespace ReadNest.Utils;

public static class BookUtils
{
    public static async Task<string> SaveBookCover(IFormFile file, IWebHostEnvironment env)
    {
        var uploadsDir = Path.Combine(env.WebRootPath ?? "wwwroot", "book-covers");
        Directory.CreateDirectory(uploadsDir);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsDir, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }
}

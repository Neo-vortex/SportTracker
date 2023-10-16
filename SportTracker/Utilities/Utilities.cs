namespace SportTracker.Utilities;

public static class Utilities
{
    public static bool IsImage(IFormFile file)
    {
        // Define the allowed image content types
        string[] allowedImageTypes = { "image/jpeg", "image/jpg", "image/png" };

        // Check if the content type is in the list of allowed image types
        if (! allowedImageTypes.Contains(file.ContentType))
        {
            return false;
        }

        var extension = Path.GetExtension(file.FileName).ToLower();

        // Define the allowed image extensions
        string[] allowedImageExtensions = { ".jpg", ".jpeg", ".png" };

        // Check if the extension is in the list of allowed image extensions
        if (!allowedImageExtensions.Contains(extension))
        {
            return false;
        }

        // Read the first few bytes of the file to check the file header
        using var reader = new BinaryReader(file.OpenReadStream());
        var headerBytes = new byte[8]; // Adjust the number of bytes based on the specific image format

        reader.Read(headerBytes, 0, headerBytes.Length);

        // Check for common image file headers
        return IsJpeg(headerBytes) || IsPng(headerBytes);
    }

    private static  bool IsJpeg(IReadOnlyList<byte> header)
    {
        // JPEG file header
        var jpegHeader = new byte[] { 0xFF, 0xD8, 0xFF };
        return !jpegHeader.Where((t, i) => header[i] != t).Any();
    }

    private static bool IsPng(byte[] header)
    {
        if (header == null) throw new ArgumentNullException(nameof(header));
        // PNG file header
        var pngHeader = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        return !pngHeader.Where((t, i) => header[i] != t).Any();
    }
}
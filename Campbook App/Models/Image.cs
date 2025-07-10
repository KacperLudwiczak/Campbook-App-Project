public class Image
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public string? Filename { get; set; }

    // Equivalent to virtual `thumbnail`
    public string? Thumbnail => Url?.Replace("/upload", "/upload/w_200");
}

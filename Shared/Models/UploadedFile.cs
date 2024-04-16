namespace TestCoreHosted.Shared.Models
{
    public class SaveFile
    {
        public List<UploadedFile> Files { get; set; }
    }
    public class UploadedFile
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public byte[] FileContent { get; set; }
    }
}

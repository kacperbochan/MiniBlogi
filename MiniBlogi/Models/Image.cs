namespace MiniBlogi.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileFormat { get; set; }
        public string FilePath { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}

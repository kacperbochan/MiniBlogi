using System.ComponentModel.DataAnnotations;

namespace MiniBlogi.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = String.Empty;

        public DateTime? DatePosted { get; set; }
        public string? IPAddress { get; set; }
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
        public BlogPost? BlogPost { get; set; }
        public int? BlogPostId { get; set; }
    }
}

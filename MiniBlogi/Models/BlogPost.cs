using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MiniBlogi.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(1)]
        public string Title { get; set; } = String.Empty;

        [Required]
        [StringLength(256)]
        [MinLength(1)]
        public string Description { get; set; } = String.Empty;

        
        public ICollection<Tag>? Tags { get; set; } = new List<Tag>();

        public ICollection<Image>? Images { get; set; } = new List<Image>();
        
        public ApplicationUser? User { get; set; }
        
        public string? UserId { get; set; }
        
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
    }
}

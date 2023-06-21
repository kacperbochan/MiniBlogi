using MiniBlogi.Models;
using System.ComponentModel.DataAnnotations;

namespace MiniBlogi.Pages.Models
{
    public class BlogPostMini
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        [StringLength(256)]
        [MinLength(1)]
        public string Description { get; set; }

        public string? Tags { get; set; } = string.Empty;

        public string? UserId { get; set; } = "";

        public ICollection<string?>? Images { get; set; } = new List<string?>();
    }
}

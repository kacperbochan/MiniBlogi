using System.ComponentModel.DataAnnotations;

namespace MiniBlogi.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public ICollection<Image> Images { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}

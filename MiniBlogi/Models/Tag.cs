using System.ComponentModel.DataAnnotations;

namespace MiniBlogi.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace MiniBlogi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<BlogPost> Posts { get; set; }
    }
}

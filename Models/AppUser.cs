using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IBlogWebApp.Models
{
    public class AppUser : IdentityUser
    {
        [Key]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}

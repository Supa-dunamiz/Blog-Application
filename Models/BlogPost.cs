using IBlogWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBlogWebApp.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdated {  get; set; }
        public string? Author { get; set; }
        public BlogPostCategory? BlogPostCategory { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}

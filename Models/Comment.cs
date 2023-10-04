using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBlogWebApp.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Author { get; set; }

        [ForeignKey("BlogPost")]
        public int? BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
    }
}

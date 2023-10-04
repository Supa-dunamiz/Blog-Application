using IBlogWebApp.Data.Enum;
using IBlogWebApp.Models;

namespace IBlogWebApp.ViewModels
{
    public class EditBlogViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? Image { get; set; }
        public string? URL { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Author { get; set; }
        public BlogPostCategory? BlogPostCategory { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public string? AppUserId { get; set; }
    }
}

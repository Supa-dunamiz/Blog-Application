using IBlogWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBlogWebApp.ViewModels
{
    public class CreateCommentViewModel
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Author { get; set; }
        public int? BlogPostId { get; set; }
    }
}


using IBlogWebApp.Data.Enum;
using IBlogWebApp.Models;

namespace IBlogWebApp.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
        Task<BlogPost> GetByIdAsync(int id);
        Task<BlogPost> GetByIdNoTrackingAsync(int id);
        Task<IEnumerable<BlogPost>> GetBlogPostByCategory(BlogPostCategory category);
        Task<IEnumerable<BlogPost>> GetBlogByAuthor(string author);
        Task<IEnumerable<BlogPost>> GetBlogPostByUser(string id);
        bool Add(BlogPost blogPost);
        bool Update(BlogPost blogPost);
        bool Delete(BlogPost blogPost);
        bool Save();
    }
}

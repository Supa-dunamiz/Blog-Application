using IBlogWebApp.Data;
using IBlogWebApp.Data.Enum;
using IBlogWebApp.Interfaces;
using IBlogWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace IBlogWebApp.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly AppDbContext _context;

        public BlogPostRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            return Save();
        }

        public bool Delete(BlogPost blogPost)
        {
            _context.Remove(blogPost);
            return Save();
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _context.BlogPosts.OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetBlogByAuthor(string author)
        {
            return await _context.BlogPosts.Where(a => a.Author.ToLower() == author.ToLower()).
                OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPostByCategory(BlogPostCategory category)
        {
            return await _context.BlogPosts.Where(c => c.BlogPostCategory == category).
                OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPostByUser(string id)
        {
            return await _context.BlogPosts.Where(u => u.AppUserId == id).
                OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _context.BlogPosts.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<BlogPost> GetByIdNoTrackingAsync(int id)
        {
            return await _context.BlogPosts.Include(c => c.Comments).AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
                
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(BlogPost blogPost)
        {
            _context.Update(blogPost);
            return Save();
        }
    }
}

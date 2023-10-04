using IBlogWebApp.Data;
using IBlogWebApp.Interfaces;
using IBlogWebApp.Models;
using System.Reflection.Metadata.Ecma335;

namespace IBlogWebApp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(Comment comment)
        {
            _context.Comments.Add(comment);
            return Save();
        }

        public bool Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Comment comment)
        {
            _context.Comments.Update(comment);
            return Save();  
        }
    }
}

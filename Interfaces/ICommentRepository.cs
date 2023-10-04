using IBlogWebApp.Models;

namespace IBlogWebApp.Interfaces
{
    public interface ICommentRepository
    {
        bool Add(Comment comment);
        bool Update(Comment comment);
        bool Delete(Comment comment);
        bool Save();
    }
}

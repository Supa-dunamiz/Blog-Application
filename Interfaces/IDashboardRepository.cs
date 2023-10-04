using IBlogWebApp.Models;

namespace IBlogWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<BlogPost>> GetAllUserBlogs();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}

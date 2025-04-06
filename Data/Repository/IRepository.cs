using Blogs.Models;

namespace Blogs.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts(int id);
        void AddPost(Post post);
        void RemovePost(int id);
        void UpdatePost(Post post);


        Task<bool> SaveChangesAsync();
       
    }
}

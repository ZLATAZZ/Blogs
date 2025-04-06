using Blogs.Models;
using System.Linq;

namespace Blogs.Data.Repository
{
    public class Repository : IRepository
    {
        private ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {

            _context = context;

        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
           
        }

        public List<Post> GetAllPosts(int id)
        {
            return _context.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _context.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }

       
    }
}

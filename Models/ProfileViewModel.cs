using Microsoft.AspNetCore.Identity;

namespace Blogs.Models
{
    public class ProfileViewModel
    {
        public IdentityUser User { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}

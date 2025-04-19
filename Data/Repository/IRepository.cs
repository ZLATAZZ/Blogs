﻿using Blogs.Models;
using Microsoft.AspNetCore.Identity;

namespace Blogs.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        void AddPost(Post post);
        void RemovePost(int id);
        void UpdatePost(Post post);


        Task<bool> SaveChangesAsync();

        List<IdentityUser> GetAllUsers();

    }
}

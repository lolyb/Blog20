using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog21.Models;

namespace Blog21.Data.Repository
{
    public interface IRepository
    {
        public Post GetPost(int id);
        // List<Post> GetAllPosts(int i);
        List<Post> GetAllPosts();
        void AddPost(Post post);
        void RemovePost(int id);
        void UpdatePost(Post post);

        Task<bool> SaveChangesAsync();
    }
}

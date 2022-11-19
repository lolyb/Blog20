﻿using Blog21.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Blog21.Helpers;
//using Blog21.Models.Comments;
using Blog21.ViewModels;
using Microsoft.EntityFrameworkCore;



namespace Blog21.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx;
        
        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        
        
        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
            
        }

        public List<Post> GetAllPosts()
        {
            return _ctx.Posts.ToList();
        
        }
        public List<Post> GetAllPosts(string category)
        {
            Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); };
            return _ctx.Posts.Where(post => InCategory(post)).ToList() ;

        }



        public Post GetPost(int id)
        {
            return _ctx.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
                 }

        public async Task <bool> SaveChangesAsync() 
        {
             if(await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        
        }
    }
}

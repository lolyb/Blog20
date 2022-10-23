using Blog21.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog21.Data.Repository;
//using Blog21.Data.FileManager;
//using Blog21.ViewModels;
using System;
using System.Collections.Generic;



namespace Blog21.Controllers
{
    public class HomeController: Controller
    {
        //private AppDbContext _ctx;

        private IRepository _repo;
        public HomeController(IRepository repo)
        {
            _repo = repo;

        }


        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }
       
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new Post());
            }
            else
            {
                var post = _repo.GetPost((int) id);
                return View(post);
            }
            
           // return View(new Post());
        }


        /*[HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
              if (post.Id > 0)
               _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())

                return RedirectToAction("Index");
            else
                return View(post);
        }


        [HttpGet]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();

            return RedirectToAction("Index");
           
        }*/

    }
}

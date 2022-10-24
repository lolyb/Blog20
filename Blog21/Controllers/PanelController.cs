using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Blog21.Data.FileManager;
using Blog21.Data.Repository;
using Blog21.Models;
using Blog21.ViewModels;
using System.Threading.Tasks;

namespace Blog21.Controllers
{

    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {

        private IRepository _repo;
        

           public PanelController(IRepository repo)
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
                var post = _repo.GetPost((int)id);
                return View(post);
            }

            // return View(new Post());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())

                return RedirectToAction("Index");
                //return RedirectToAction("Index, Panel");

            else
                return View(post);
        }


        [HttpGet]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }
}

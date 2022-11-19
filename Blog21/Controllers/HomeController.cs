using Blog21.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog21.Data.Repository;
using Blog21.Data.FileManager;
using Blog21.ViewModels;
using System;
using System.Collections.Generic;



namespace Blog21.Controllers
{
    public class HomeController: Controller
    {
        private IFileManager _fileManager;
        private IRepository _repo;
     
        public HomeController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }


        public IActionResult Index(string category)
        {
            var posts = string.IsNullOrEmpty(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category);
            //boolean ? tru: false;
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }
       

        [HttpGet("/Image/{image}")]
          public IActionResult Image(string image)
          {
            var mime = image.Substring(image.LastIndexOf('.') + 1); 
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
             
          }

       


        

    }
}

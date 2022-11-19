using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Blog21.Data.FileManager;
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
        private IFileManager _fileManager;
        

           public PanelController(IRepository repo, IFileManager fileManager)
           {
                _repo = repo;
                _fileManager = fileManager;
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
                return View(new PostViewModel());
            }
            var post = _repo.GetPost((int)id);
            return View(new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                CurrentImage = post.Image,
                Description = post.Description,
                Category= post.Category,
                Tags= post.Tags

            }) ;

            

            // return View(new Post());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Description= vm.Description,
                Category = vm.Category,
                Tags= vm.Tags,
               };
            
            
               if(vm.Image == null)
               {
                post.Image = "Same";
               // post.Image = vm.CurrentImage;
                }
                else
                {
                   post.Image = await _fileManager.SaveImage(vm.Image); //Handle Image
                
                }


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

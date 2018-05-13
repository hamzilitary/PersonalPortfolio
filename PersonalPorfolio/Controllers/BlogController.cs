using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PersonalPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalPortfolio.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var associatedComment = _db.Comments.Include(c => c.AssociatedBlog).FirstOrDefault( c => c.CommentKey == id);
            var blogId = associatedComment.AssociatedBlog.BlogKey;
            if (associatedComment != null)
            {
                _db.Comments.Remove(associatedComment);
                _db.SaveChanges();                
            }
            return PartialView("Comment",_db.Comments.Include(c => c.AssociatedBlog).Where(c => c.AssociatedBlog.BlogKey == blogId).ToList());
        }
       

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Blogs.Include(b => b.Comments).Where(x => x.User.Id == currentUser.Id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            blog.User = currentUser;
            _db.Blogs.Add(blog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            if (!string.IsNullOrWhiteSpace(comment.CommentText) && comment.BlogKey != default(int)) {
                var blog = _db.Blogs.Find(comment.BlogKey);
                comment.ReplyDtm = DateTime.Now;
                comment.AssociatedBlog = blog;
                _db.Comments.Add(comment);

                await _db.SaveChangesAsync();
            }
            return PartialView("Comment",_db.Comments.Where(c => c.AssociatedBlog.BlogKey == comment.BlogKey).ToList());
        }

        

        public IActionResult HelloAjax()
        {
            return Content("Hello from the controller!", "text/plain");
        }
        public IActionResult Blog()
        {
            return View();
        }
      
    }
}
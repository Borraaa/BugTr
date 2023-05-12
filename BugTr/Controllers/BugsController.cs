using BugTr.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace BugTr.Controllers
{
    public class BugsController : Controller
    {

        private readonly BugTrackerContext _context;

        public BugsController(BugTrackerContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var users = _context.Users.ToList(); // Kullanıcıları veritabanından alın
            ViewBag.Users = users; // Kullanıcıları Index sayfasına gönderin

            var bugs = _context.Bugs.Include(b => b.User).Include(b => b.Project).ToList(); // Hataları veritabanından alın

            return View();
        }


        //--------------CREATE--------------------

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Projects = _context.Projects.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Bug bug)
        {
            _context.Bugs.Add(bug);
            _context.ChangeTracker.DetectChanges();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        //------------_EDİT_------------------

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var bug = _context.Bugs
                              .Include(b => b.User)
                              .Include(b => b.Project)
                              .Include(b => b.Comments) // Yorumları da dahil et
                              .ThenInclude(c => c.User) // Yorumların kullanıcılarını da dahil et
                              .FirstOrDefault(b => b.Id == id);

            if (bug != null)
            {
                return NotFound();
            }

            return View(bug);
        }


        [HttpPost]
        public IActionResult Edit(int id, Bug updatedBug)
        {
            var bug = _context.Bugs
                              .Include(b => b.User)
                              .Include(b => b.Project)
                              .FirstOrDefault(b => b.Id == id);

            if (bug == null)
            {
                return NotFound();
            }

            // Yapılan değişiklikleri veritabanındaki nesneye aktar
            bug.Title = updatedBug.Title;
            bug.Status = updatedBug.Status;

            _context.SaveChanges(); // Değişiklikleri kaydet

            return RedirectToAction("Index");
        }


        //--------------- ADDCOMMENT----------------------

        [HttpGet]
        public IActionResult AddComment(int id)
        {
            var bug = _context.Bugs.Include(b => b.User).Include(b => b.Project).FirstOrDefault(b => b.Id == id);

            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }


        [HttpPost]
        public IActionResult AddComment(Bug model, string commentText)
        {
            var bug = _context.Bugs.Include(b => b.User).Include(b => b.Project).FirstOrDefault(b => b.Id == model.Id);

            if (bug == null)
            {
                return NotFound();
            }

            var comment = new Comment
            {
                BugId = model.Id,
                Text = commentText,
                CreatedAt = DateTime.Now,
                UserId = bug.UserId,
                User = bug.User
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Edit", new { id = model.Id });
        }

        //-------------DELETE-----------


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var bug = _context.Bugs.FirstOrDefault(b => b.Id == id);

            if (bug == null)
            {
                return NotFound();
            }

            _context.Bugs.Remove(bug);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}
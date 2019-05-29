using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BrightIdeas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace brightIdeas.Controllers
{
    public class IdeaController : Controller
    {
        private Context _context;
        public IdeaController(Context context)
        {
            _context = context;
        }

        [HttpGet("Main")]
        public IActionResult Main()
        {
            string loggedUser = HttpContext.Session.GetString("Email");
            User creator = _context.Users.SingleOrDefault(user => user.Email == loggedUser);
            MainVM viewModel = new MainVM()
            {
                User = creator,
                allIdeas = _context.Ideas
                    .Include(i => i.Creator)
                    .Include(i => i.Likes)
                    .ThenInclude(u => u.LikeUser)
                    .OrderByDescending(i => i.Likes.Count)
                    .ToList()
            };
            return View(viewModel);
        }

        [HttpPost("Create")]
        public IActionResult Create(Idea Idea)
        {
            if(ModelState.IsValid)
            {
                System.Console.WriteLine("Inside Create Idea Route");
                string loggedUser = HttpContext.Session.GetString("Email");
                User creator = _context.Users.SingleOrDefault(user => user.Email == loggedUser);
                Idea.Creator = creator;
                _context.Add(Idea);
                _context.SaveChanges();
                return RedirectToAction("Main", "Idea");
            }
            else
            {
                System.Console.WriteLine("Failed Idea Create Validation");
                return RedirectToAction("Main", "Idea");
            }
        }

        [HttpGet("Delete/{IdeaId}")]
        public IActionResult Delete(int IdeaId)
        {
            Idea ideaToDelete = _context.Ideas.SingleOrDefault(i => i.IdeaId == IdeaId);
            _context.Ideas.Remove(ideaToDelete);
            _context.SaveChanges();
            return RedirectToAction("Main", "Idea");
        }

        [HttpGet("Like/{IdeaId}")]
        public IActionResult Like(int IdeaId)
        {
            string loggedUser = HttpContext.Session.GetString("Email");
            User creator = _context.Users.SingleOrDefault(user => user.Email == loggedUser);
            Idea idea = _context.Ideas.SingleOrDefault(i => i.IdeaId == IdeaId);
            Like newLike = new Like()
            {
                UserId = creator.UserId,
                IdeaId = idea.IdeaId
            };
            _context.Add(newLike);
            _context.SaveChanges();
            return RedirectToAction("Main", "Idea");
        }

        [HttpGet("UserShow/{UserId}")]
        public IActionResult UserShow(int UserId)
        {
            User user = _context.Users
                .Include(u => u.CreatedIdeas)
                .SingleOrDefault(u => u.UserId == UserId);
            List<Like> likes = _context.Likes
                .Include(l => l.LikedIdea)
                .ThenInclude(i => i.Creator)
                .Where(i => i.LikedIdea.Creator.UserId == UserId)
                .ToList();
            UserVM viewModel = new UserVM()
            {
                User = user,
                allLikes = likes
            };
            return View(viewModel);
        }

        [HttpGet("IdeaShow/{IdeaId}")]
        public IActionResult IdeaShow(int IdeaId)
        {
            Idea viewModel = _context.Ideas
                .Include(i => i.Creator)
                .Include(i => i.Likes)
                .ThenInclude(l => l.LikeUser)
                .SingleOrDefault(i => i.IdeaId == IdeaId);
            return View(viewModel);
        }
    }
}
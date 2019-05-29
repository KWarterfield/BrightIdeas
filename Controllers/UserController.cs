using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BrightIdeas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BrightIdeas.Controllers
{
    public class UserController : Controller
    {

        private Context _context;

        public UserController(Context context)
        {
            _context = context;
        }

        [HttpGet("")]

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

                [HttpPost("Register")]
        public IActionResult Register(User newUser)
        {
            System.Console.WriteLine("Inside Register Route");
            if(ModelState.IsValid)
            {
                System.Console.WriteLine("Model State is Valid");
                if(_context.Users.Any(u => u.Email == newUser.Email))
                {
                    System.Console.WriteLine("Email already exists");
                    ModelState.AddModelError("newUser.Email", "Email already in use!");
                    return View("Index");
                }
                else
                {
                    System.Console.WriteLine("Adding User, passed validations");
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    _context.Add(newUser);
                    _context.SaveChanges();
                    HttpContext.Session.SetString("Email", newUser.Email); 
                    return RedirectToAction("Main", "Idea");
                }
            }
            else
            {
                System.Console.WriteLine("Failed initial validation");
                return View("Index");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUser existing)
        {
            if(ModelState.IsValid)
            {
                System.Console.WriteLine("Model State is Valid");
                var userInDb = _context.Users.FirstOrDefault(u => u.Email == existing.Email);
                if(userInDb == null)
                {
                    System.Console.WriteLine("Inside Login user does not exist");
                    ModelState.AddModelError("existing.Email", "Invalid Email/Password");
                    return View("Index");
                }
                else
                {
                    var hasher = new PasswordHasher<LoginUser>();
                    var result = hasher.VerifyHashedPassword(existing, userInDb.Password, existing.Password);
                    if(result == 0)
                    {
                        System.Console.WriteLine("Password error");
                        ModelState.AddModelError("Password", "Invalid Email/Password");
                    }
                    User selectedUser = _context.Users.FirstOrDefault(User=>User.Email == existing.Email);
                    HttpContext.Session.SetString("Email", selectedUser.Email); 
                    System.Console.WriteLine("Passed validations, logging in");
                    return RedirectToAction("Main", "Idea");
                }
            }
            System.Console.WriteLine("failed login validations");
            return View("Index");
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
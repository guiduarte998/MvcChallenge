using Data.Interface;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MvcChallenge.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userContext;

        public UserController(IUserRepository userContext)
        {
            _userContext = userContext;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user, string passwordConfirmation)
        {
            if (ModelState.IsValid)
            {
                // Check if the password and password confirmation match
                if (user.PasswordHash != passwordConfirmation)
                {
                    ModelState.AddModelError("PasswordConfirmation", "Password and confirmation do not match.");
                    return View(user);
                }

                if (!_userContext.UserExists(user.Email))
                {
                    _userContext.AddUser(user);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "User already exists.");
                }
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = _userContext.GetUserByEmailAndPassword(email, password);
                if (user != null)
                {
                    return RedirectToAction("Index", "Home"); // Redirect to the desired page after login
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View();
        }
    }
}

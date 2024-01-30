using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;

namespace MvcChallenge.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorRepository _authorContext;

        public AuthorController(IAuthorRepository authorContext)
        {
            _authorContext = authorContext;
        }



        // GET: Author
        public IActionResult Index()
        {
            var authors = _authorContext.GetAll();
            return View(authors);
        }

        public IActionResult Details(Guid id)
        {
            var author = _authorContext.GetAuthorById(id);
            return View(author);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public IActionResult Create([Bind] Author author)
        {
            try
            {
                _authorContext.AddAuthor(author);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Edit/
        public IActionResult Edit(Guid id)
        {
            var author = _authorContext.GetAuthorById(id);
            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        public IActionResult Edit(Guid id, [Bind] Author author)
        {
            try
            {
                if (id != author.Id)
                {
                    return NotFound();
                }

                _authorContext.UpdateAuthor(author);
                return RedirectToAction("Index"); 
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete
        public IActionResult Delete(Guid id)
        {
            _authorContext.DeleteAuthor(id);
            return RedirectToAction("Index");
        }
    }
}

using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MvcChallenge.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository _bookContext;

        public BookController(IBookRepository bookContext)
        {
            _bookContext = bookContext;
        }
        // GET: Book
        public IActionResult Index()
        {
            var books = _bookContext.GetAll();
            return View(books);
        }

        public IActionResult Details(Guid id)
        {
            var book = _bookContext.GetBookById(id);
            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public IActionResult Create([Bind] Book book)
        {
            try
            {
                _bookContext.AddBook(book);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/
        public IActionResult Edit(Guid id)
        {
            var book = _bookContext.GetBookById(id);
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public IActionResult Edit(Guid id, [Bind] Book book)
        {
            try
            {
                if (id != book.Id)
                {
                    return NotFound();
                }

                _bookContext.UpdateBook(book);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete
        public IActionResult Delete(Guid id)
        {
            _bookContext.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}

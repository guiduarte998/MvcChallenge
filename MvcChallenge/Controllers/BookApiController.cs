using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Request;
using Newtonsoft.Json;
using System.Text;

namespace MvcChallenge.Controllers
{
    public class BookApiController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://devtestapi.debtstream.co.uk/Book/GetAll");
            request.Headers.Add("API-Key", "1234567890");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<List<BookApi>>(data);
                return View(books);
            }

            return NoContent(); // Or any other error handling
        }


        // GET: BookApi/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Book/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind] BookApiRequest book)
        {
            var json = JsonConvert.SerializeObject(book);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://devtestapi.debtstream.co.uk/Book/Add")
            {
                Content = content
            };
            request.Headers.Add("API-Key", "1234567890");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Book/id
        public async Task<IActionResult> Details(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://devtestapi.debtstream.co.uk/Book/Get/{id}");
            request.Headers.Add("API-Key", "1234567890");

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var book = JsonConvert.DeserializeObject<BookApi>(data);
                return View(book);
            }

            return NoContent();
        }

        // GET: Book/Edit/
        public async Task<IActionResult> Edit(string id)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://devtestapi.debtstream.co.uk/Book/Get/{id}");
            request.Headers.Add("API-Key", "1234567890");

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var book = JsonConvert.DeserializeObject<BookApi>(data);
                return View(book);
            }

            return NoContent();
        }


        // POST: Book/Edit/id
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind] BookApi book)
        {

            var json = JsonConvert.SerializeObject(book);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://devtestapi.debtstream.co.uk/Book/Edit")
            {
                Content = content
            };
            request.Headers.Add("API-Key", "1234567890");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(book);
        }
    }
}

using System.Net.Http;
using Newtonsoft.Json;
using Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Text;  // Assuming this is where your Author model is defined

public class AuthorApiController : Controller
{
    private readonly HttpClient _httpClient;

    public AuthorApiController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET: Author
    public async Task<IActionResult> Index()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "https://devtestapi.debtstream.co.uk/Author/GetAll");
        request.Headers.Add("API-Key", "1234567890");

        HttpResponseMessage response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            var authors = JsonConvert.DeserializeObject<List<AuthorApi>>(data);
            return View(authors);
        }

        return NoContent(); // Or any other error handling
    }


    // GET: AuthorApi/Create
    public IActionResult Create()
    {
        return View();
    }

    //POST: Author/Create
    [HttpPost]
    public async Task<IActionResult> Create([Bind] AuthorApi author)
    {
        var json = JsonConvert.SerializeObject(author);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, $"https://devtestapi.debtstream.co.uk/Author/Add")
        {
            Content = content
        };
        request.Headers.Add("API-Key", "1234567890");

        HttpResponseMessage response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View(author);
    }

    // GET: Author/id
    public async Task<IActionResult> Details(string id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://devtestapi.debtstream.co.uk/Author/Get/{id}");
        request.Headers.Add("API-Key", "1234567890");

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            var author = JsonConvert.DeserializeObject<AuthorApi>(data);
            return View(author);
        }

        return NoContent();
    }

    // GET: Author/Edit/
    public async Task<IActionResult> Edit(string id)
    {

        var request = new HttpRequestMessage(HttpMethod.Get, $"https://devtestapi.debtstream.co.uk/Author/Get/{id}");
        request.Headers.Add("API-Key", "1234567890");

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            var author = JsonConvert.DeserializeObject<AuthorApi>(data);
            return View(author);
        }

        return NoContent();
    }


    // POST: Author/Edit/id
    [HttpPost]
    public async Task<IActionResult> Edit(string id, [Bind] AuthorApi author)
    {

        var json = JsonConvert.SerializeObject(author);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, $"https://devtestapi.debtstream.co.uk/Author/Edit/{id}")
        {
            Content = content
        };
        request.Headers.Add("API-Key", "1234567890");

        HttpResponseMessage response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View(author);
    }


    //I tried to implement a delete method but I notice later that the EndPoint didn't exist...

    //[HttpPost]
    //public async Task<IActionResult> Delete(string id)
    //{
    //    HttpResponseMessage response = await _httpClient.DeleteAsync($"Author/Delete/{id}");

    //    if (response.IsSuccessStatusCode)
    //    {
    //        return RedirectToAction("Index");
    //    }

    //    return View("Error");
    //}
}

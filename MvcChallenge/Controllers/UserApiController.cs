using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Request;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace MvcChallenge.Controllers
{
    public class UserApiController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRequest user, string passwordConfirmation)
        {
            if (ModelState.IsValid)
            {
                // Check if the password and password confirmation match
                if (user.PasswordHash != passwordConfirmation)
                {
                    ModelState.AddModelError("PasswordConfirmation", "Password and confirmation do not match.");
                    return View(user);
                }

                // Hash the password
                user.PasswordHash = ComputeSha256Hash(passwordConfirmation);

                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, $"https://devtestapi.debtstream.co.uk/Auth/RegisterUser")
                {
                    Content = content
                };
                request.Headers.Add("API-Key", "1234567890");

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "User registration failed.");
                }
            }
            return View(user);
        }



        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login(string email)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://devtestapi.debtstream.co.uk/Auth/GetUserByEmail/{email}");
                request.Headers.Add("API-Key", "1234567890");

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<UserRequest>(data);

                    if (user != null) 
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error while retrieving user.");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://devtestapi.debtstream.co.uk/Auth/GetUser/{id}");
                request.Headers.Add("API-Key", "1234567890");

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<UserRequest>(data);

                    if (user != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error while retrieving user.");
                }
            }
            return View();
        }


        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}

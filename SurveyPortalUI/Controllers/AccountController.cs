using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace SurveyPortal.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5001/"); // API adresin
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/Login", new { Username = username, Password = password });
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("token", token); // Oturuma token yazılıyor
                return RedirectToAction("Index", "Surveys");
            }
            ViewBag.Error = "Login failed";
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/Register", new { Username = username, Password = password });
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            ViewBag.Error = "Register failed";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

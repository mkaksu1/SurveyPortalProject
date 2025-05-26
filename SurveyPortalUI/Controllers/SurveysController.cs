using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using SurveyPortal.MVC.Models;

namespace SurveyPortal.MVC.Controllers
{
    public class SurveysController : Controller
    {
        private readonly HttpClient _httpClient;

        public SurveysController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5001/"); // API URL'ini kendi adresine göre güncelle
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Survey");
            if (response.IsSuccessStatusCode)
            {
                var surveys = await response.Content.ReadFromJsonAsync<List<Survey>>();
                return View(surveys);
            }
            return View(new List<Survey>());
        }

        public async Task<IActionResult> Take(int id)
        {
            var response = await _httpClient.GetAsync($"api/Survey/{id}");
            if (response.IsSuccessStatusCode)
            {
                var survey = await response.Content.ReadFromJsonAsync<Survey>();
                return View(survey);
            }
            return RedirectToAction("Index");
        }
    }
}

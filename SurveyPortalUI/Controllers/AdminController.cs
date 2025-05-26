using Microsoft.AspNetCore.Mvc;
using SurveyPortal.MVC.Models;
using System.Net.Http.Json;

namespace SurveyPortal.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5001/"); // kendi API adresine göre ayarla
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Survey");
            var surveys = await response.Content.ReadFromJsonAsync<List<Survey>>();
            return View(surveys);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Survey survey)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Survey", survey);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Survey/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Results(int id)
        {
            // Gerçek API'den veri çekilmeli. Şimdilik örnek JSON kullanılabilir.
            // Eğer endpoint yoksa statik veri ile çalışır halde göstermek yeterli.

            var sampleResults = new List<ChartResult>
    {
        new ChartResult { Label = "Evet", Count = 15 },
        new ChartResult { Label = "Hayır", Count = 7 },
        new ChartResult { Label = "Kararsız", Count = 3 }
    };

            return View(sampleResults);
        }


        // Edit sayfası da eklenebilir
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BuildABandHub.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace BuildABandHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
            string Baseurl = "https://localhost:44325/";
            List<Concert> Concerts = new List<Concert>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LocalShows()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("api/concert/");
                if(responseMessage.IsSuccessStatusCode)
                {
                    var ApiResponse = responseMessage.Content.ReadAsStringAsync().Result;

                    Concerts = JsonConvert.DeserializeObject<List<Concert>>(ApiResponse);
                }
            }
                return View(Concerts);
        }
        [HttpPost]
        public async Task<IActionResult> LocalShows(string id)
        {
            try
            {
                id.Trim();
                if (id.Contains(" "))
                {
                   id = id.Replace(" ", "/");

                }
                else if (id.Contains(","))
                {
                    id = id.Replace(",", "/");
                }


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = await client.GetAsync("api/concert/" + id);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var ApiResponse = responseMessage.Content.ReadAsStringAsync().Result;

                        Concerts = JsonConvert.DeserializeObject<List<Concert>>(ApiResponse);
                    }
                }
                return View(Concerts);
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("api/concert/");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var ApiResponse = responseMessage.Content.ReadAsStringAsync().Result;

                    Concerts = JsonConvert.DeserializeObject<List<Concert>>(ApiResponse);
                }
            }

            return View(Concerts.Where(c=>c.ConcertId == id).FirstOrDefault());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

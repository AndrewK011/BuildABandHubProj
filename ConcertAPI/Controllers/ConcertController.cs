using ConcertAPI.Data;
using ConcertAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConcertAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertController : ControllerBase
    {
        private ApplicationContext _context;
        public ConcertController(ApplicationContext context)
        {
            _context = context;
        }
            Coordinates coordinates = new Coordinates();
        // GET
        [HttpGet]
        public IActionResult Get()
        {
            var concerts = _context.Concerts.ToList();

            return Ok(concerts);
        }

        // GET
        [Route("{city}/{state}")]
        [HttpGet]
        public async Task<IActionResult> Get(string city, string state)
        {
            await GetCoordinates(city, state);
            List<Concert> returnedConcerts = new List<Concert>();
            foreach (Concert concert in _context.Concerts)
            {
                if (GetDistance(coordinates.latitude,coordinates.longitude, concert.Lat, concert.Long) <= 50)
                {
                    returnedConcerts.Add(concert);
                }
            }
            return Ok(returnedConcerts);
        }

        public async Task<Coordinates> GetCoordinates(string city, string state)
        {
            string url = string.Format(
               "https://maps.googleapis.com/maps/api/geocode/json?address={0},+{1}&key={2}",
                city, state, ApiKeys.googleApi);

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                JObject jObject = JObject.Parse(jsonResult);

                var results = jObject["results"];
                coordinates.latitude = (double)results[0]["geometry"]["location"]["lat"];
                coordinates.longitude = (double)results[0]["geometry"]["location"]["lng"];
                return coordinates;
            }
            else
            {
                coordinates.latitude = 0;
                coordinates.longitude = 0;
                return coordinates;
            }
        }

        public double GetDistance(double latitude, double longitude, double otherLatitude, double otherLongitude)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            var meters = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
            var kilometers = meters / 1000;
            return kilometers * 0.6214;
        }
    }
}
using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Properties;
using WebApplication2.Services;

namespace WebApplication2.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(IUniversalReqService reqService) : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                
            {
                salom = "salom " + index,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet( "countries",Name = "getCountry")]
    public ApiResponse<List<Country>> getCountry()
    {
        string username = "AZMIDDIN_DEV";
        string password = "azmiddin2007";
        var authBytes = Encoding.ASCII.GetBytes($"{username}:{password}");
        var authValue = Convert.ToBase64String(authBytes);

        new Dictionary<String, String>().Add("Content-Type", "application/json");
        
        var headers = new Dictionary<string, string>
        {
            { "Authorization", $"Basic {authValue}" },
            { "Content-Type", "application/json" }
        };

        
       return reqService.GetRequest<ApiResponse<List<Country>>>(
            "https://online.apexlife.uz/apex/ins/apex/life/web/api/get_all_country",headers ).Result;
    }
}
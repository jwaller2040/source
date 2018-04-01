using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private const string BaseUrl = @"http://api.openweathermap.org/data/2.5/weather?zip=35222%2Cus&APPID=bd9dc5b984767a2a4977afdcffc0b210&units=metric";
      
        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {

            var client = new RestClient(BaseUrl);
            var request = new RestRequest(Method.GET);

            request.AddHeader("postman-token", "3995c813-9f37-b0f9-d8de-83ba7004d150");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");


            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            var jsonResponse = JsonConvert.DeserializeObject<Rootobject>(response.Content);
            if (jsonResponse == null)
            {
                jsonResponse = new Rootobject() { cod = 5, main = new Main() { temp = 99 }, weather = new Weather[] { new Weather() { description ="Broken" } } };
                
            }
            return Enumerable.Repeat(new WeatherForecast
            {
                DateFormatted = DateTime.Now.ToString("d"),
                TemperatureC = jsonResponse.main.temp,
                Summary = jsonResponse?.weather[0]?.main
            }, 1);
        
        }
    
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public float TemperatureC { get; set; }
            public string Summary { get; set; }

            public float TemperatureF
            {
                get
                {
                    return (float)Math.Round(32 + (float)(TemperatureC / 0.5556),1);
                }
            }
        }
    }
}

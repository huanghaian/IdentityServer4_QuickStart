using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyMvcClient.Models;
using Newtonsoft.Json.Linq;

namespace MyMvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            //注销，转到认证server 注销
            return SignOut("Cookies", "oidc");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> CallApi()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            //accessToken = $"eyJhbGciOiJSUzI1NiIsImtpZCI6IjRHNFBDQmtqLS1YUnI1dE12ZUc1U3ciLCJ0eXAiOiJhdCtqd3QifQ.eyJuYmYiOjE1OTIxMDkzNzEsImV4cCI6MTU5MjExMjk3MSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSIsImF1ZCI6ImFwaTEiLCJjbGllbnRfaWQiOiJjbGllbnQiLCJzY29wZSI6WyJhcGkxIl19.fRP_R5FE8r3eznk7vP03Jryo3gsypPpAtK4-kNmmGYWtdNqHLgpEX9Te4bE_6TWoCkczMVgeJMhnfFdBpw7rrMqJlLNMljCXZKSivY1x7phYJPOMZR-VFqNXF8GwGYTrVBxOomoqsJEbNJeDxt6m5Nv_9jjOnpuyITrMwbVOBhx5_sc5k590lXvoWFzYNLum4oS_jlfsy3AXK4kkC9Lyh0LKO7ZSRTOUc5MJxXck4zya29U-bJAs63zVMrPkQJU15wDzMjk1Q9Pvb8ihmuJibWHZecSSSKQzx70vN8s1fP2FjDo3sl6OoKPkKNfuxwBKaWIPX7X3r9ytGqI1YrwrNg";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.GetStringAsync("https://localhost:6001/api/Identity");
            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

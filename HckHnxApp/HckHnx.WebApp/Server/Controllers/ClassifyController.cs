using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HckHnx.WebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassifyController : ControllerBase
    {
        private readonly HttpClient _cameraClient;

        public ClassifyController(HttpClient cameraClient)
        {
            _cameraClient = cameraClient;
        }
        public async Task<string> Index()
        {
            byte[] byteArray = Encoding.ASCII.GetBytes("admin:sdi");
            _cameraClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await _cameraClient.GetAsync("/vapps/classifier/resultsources/last");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }

            return "error";

        }
    }
}

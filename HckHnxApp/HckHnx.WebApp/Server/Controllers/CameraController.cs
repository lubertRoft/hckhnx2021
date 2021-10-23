using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HckHnx.WebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CameraController : ControllerBase
    {
        private readonly HttpClient _cameraClient;

        public CameraController(HttpClient cameraClient)
        {
            _cameraClient = cameraClient;
        }
        [Route("classify")]
        public async Task<string> GetClassification()
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
        [Route("image")]
        public async Task<IActionResult> GetImage()
        {
            byte[] byteArray = Encoding.ASCII.GetBytes("admin:sdi");
            _cameraClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await _cameraClient.GetAsync("/camera/image");

            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                return File(stream, "image/jpeg");
            }

            return BadRequest();
        }
    }
}

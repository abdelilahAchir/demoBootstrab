using Microsoft.AspNetCore.Mvc;
using sneakers_api_.net.models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sneakers_api_.net.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class SneakersController : ControllerBase
    {

        private readonly HttpClient _httpClient;

        public SneakersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("https://sneakersxu.azurewebsites.net/");
        }
        // GET: api/<SneakersController>
        [HttpGet]
        public async Task<IActionResult> Get(Sneakers sneakerList )
        {
            var response = await _httpClient.GetAsync("sneakers");

            if (response.IsSuccessStatusCode)
            {
              var snkrs =  sneakerList.GetAllSneakers();
                //var data = await response.Content.ReadAsStringAsync();
                //var sneakerList = JsonConvert.DeserializeObject<List<Sneakers>>(data);
                return Ok(snkrs);
            }
            else
            {
                return StatusCode(500, "Request to external API failed with status code: " + response.StatusCode);
            }
        }


        // POST api/<SneakersController>
        [HttpPost]
        public async Task<IActionResult> Post(Sneaker sneaker)
        {
			var json = JsonConvert.SerializeObject(sneaker);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("https://sneakersxu.azurewebsites.net/addSneakerXu", content);

			if (!response.IsSuccessStatusCode)
			{
				return Ok();
			}

			return BadRequest();

		}

        

        // DELETE api/<SneakersController>/5
        [HttpDelete("sneaker")]
        public async Task<IActionResult> Delete(Sneaker sneaker)
        {
	        var json = JsonConvert.SerializeObject(sneaker);
	        var content = new StringContent(json, Encoding.UTF8, "application/json");
	        var response = await _httpClient.PostAsync("https://sneakersxu.azurewebsites.net/deleteSneakerXu", content);

	        if (response.IsSuccessStatusCode)
	        {
		        return Ok();
	        }

	        return BadRequest();
		}
    }
}

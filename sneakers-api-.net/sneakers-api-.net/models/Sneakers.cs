using System.Text.Json;

namespace sneakers_api_.net.models
{
    public class Sneakers
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly Dictionary<int, Sneaker> _sneakers = new Dictionary<int, Sneaker>();

        public Sneakers()
        {
            FetchAllSneakers().Wait();
        }
        public async Task FetchAllSneakers()
        {

            _sneakers.Clear();

            var response = await _httpClient.GetAsync($"https://sneakersxu.azurewebsites.net/sneakers");
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<JsonElement>(json);

            for (var i = 0; i < data.GetArrayLength(); i++)
            {
                var brand = data[i].GetProperty("brand").GetString();
                var model = data[i].GetProperty("model").GetString();
                var price = data[i].GetProperty("price").GetString();

                var sizes = new List<string>();
                for (var j = 0; j < data[i].GetProperty("sizes").GetArrayLength(); j++)
                {
                    var size = data[i].GetProperty("sizes")[j].GetString();
                    if (size != null) sizes.Add(size);
                }

                var gender = data[i].GetProperty("gender").GetString();

                var colors = new List<string>();
                for (var j = 0; j < data[i].GetProperty("colors").GetArrayLength(); j++)
                {
                    var color = data[i].GetProperty("colors")[j].GetString();
                    if (color != null) colors.Add(color);
                }

                var images_urls = new List<string>();
                for (int j = 0; j < data[i].GetProperty("images_urls").GetArrayLength(); j++)
                {
                    var image_url = data[i].GetProperty("images_urls")[j].GetString();
                    if (image_url != null) images_urls.Add(image_url);
                }

                var sneaker = new Sneaker
                {
                    id = i,
                    brand = brand,
                    model = model,
                    price = price,
                    sizes = sizes.ToArray(),
                    gender = gender,
                    colors = colors.ToArray(),
                    images_urls = images_urls.ToArray()
                };

                if (!_sneakers.ContainsValue(sneaker) && !_sneakers.ContainsKey(i))
                {
                    _sneakers.Add(i, sneaker);
                }
            }
        }
        public List<Sneaker> GetAllSneakers() => _sneakers.Values.ToList();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.IO;

namespace ImageGallery
{
    class DataFetcher
    {
        //This method uses the HttpClient class to fetch the JSON data from the server.
        async Task<string> GetDatafromService(string searchstring)
        {
            string readText = null;
            try
            {
                String url = @"https://imagefetcherapi.azurewebsites.net/api/fetch_images?query=" +
               searchstring + "&max_count=5";
                using (HttpClient c = new HttpClient())
                {
                    readText = await c.GetStringAsync(url);
                }
            }
            catch
            {
                readText = File.ReadAllText(@"Data/sampleData.json");
            }
            return readText;
        }
        public async Task<List<ImageItem>> GetImageData(string search)
        {
            string data = await GetDatafromService(search);
            //Here, we use the JsonConvert.DeserializeObject() method of Newtonsoft.Json to parse the json data 
            //into an instance of ImageItem
            return JsonConvert.DeserializeObject<List<ImageItem>>(data);
        }

    }
}

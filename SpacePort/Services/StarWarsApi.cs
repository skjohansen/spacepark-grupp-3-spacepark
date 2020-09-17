using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePort.Services
{
    public class StarWarsApi
    {
        private const string Path= "https://swapi.dev/api/";
        public async Task<bool>GetDriverName(string name)
        {
            name = name.ToLower();
            var request = new RestRequest($"people/?search={name}", Method.GET);

            RestClient client = new RestClient(Path);
            var response = await client.ExecuteAsync(request);
            JObject jObject = JObject.Parse(response.Content);

            int count = (int)jObject["count"];
            if (count< 1)
            {
                return false;
            }
            else
            return true;
        }
    }
}

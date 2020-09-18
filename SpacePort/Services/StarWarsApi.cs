using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePort.Services
{
    public static class StarWarsApi
    {
        private const string Path= "https://swapi.dev/api/";

        public static async Task<bool>GetDriverName(string name)
        {
            name = name.ToLower();
            var request = new RestRequest($"people/?search={name}", Method.GET);

            RestClient client = new RestClient(Path);
            var response = await client.ExecuteAsync(request);
            JObject jObject = JObject.Parse(response.Content);

            return (int)jObject["count"] > 0;
        }
    }
}

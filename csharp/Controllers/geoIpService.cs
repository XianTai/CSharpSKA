using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog;

namespace csharp.Controllers
{
    public class geoIpService : IgeoIpService
    {
        private HttpClient _httpClient;

        public geoIpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetMyip()
        {
            var task = await _httpClient.GetAsync("https://api.ipify.org?format=json");
            task.EnsureSuccessStatusCode();
            var stringAsync = await task.Content.ReadAsStringAsync();
            var ipData = JsonSerializer.Deserialize<IpData>(stringAsync);
            return ipData.ip;
        }

        public async Task<whereAmIRespons> GetMyGeoData(string myIp)
        {
            var message = await _httpClient.PostAsync("http://ip-api.com/batch", new StringContent($"[\"{myIp}\"]"));
            message.EnsureSuccessStatusCode();
            var myGeoData = await message.Content.ReadAsByteArrayAsync();
            var geoDetail = JsonSerializer.Deserialize<List<GeoData>>(myGeoData).Single();
            return new whereAmIRespons(geoDetail.countryCode, myIp);
        }

        public async Task<whereAmIRespons> GetIpAndCountryCode()
        {
            var myip = await GetMyip();
            var myGeoData =await GetMyGeoData(myip);
            Log.Information(myGeoData.CountryCode);
            return  myGeoData;
        }

    }

    public class whereAmIRespons 
    {
        public whereAmIRespons(string countryCode, string ip)
        {
            Ip = ip;
            CountryCode = countryCode;
        }
        public string Ip { get; }
        public  string CountryCode { get; }
    }
}
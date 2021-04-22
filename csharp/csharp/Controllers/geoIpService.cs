using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace csharp.Controllers
{
    public class geoIpService : IgeoIpService
    {
        public async Task<string> GetMyip()
        {
            var httpClient = new HttpClient();
            var task = await httpClient.GetAsync("https://api.ipify.org?format=json");
            task.EnsureSuccessStatusCode();
            var stringAsync = await task.Content.ReadAsByteArrayAsync();
            var ipData = JsonSerializer.Deserialize<IpData>(stringAsync);
            return ipData.ip;
        }

        public async Task<whereAmIRespons> GetMyGeoData(string myIp)
        {
            var httpClient = new HttpClient();
            var message = await httpClient.PostAsync("http://ip-api.com/batch", new StringContent($"[\"{myIp}\"]"));
            message.EnsureSuccessStatusCode();
            var myGeoData = await message.Content.ReadAsByteArrayAsync();
            var geoDetail = JsonSerializer.Deserialize<List<GeoData>>(myGeoData).Single();
            return new whereAmIRespons(geoDetail.countryCode, myIp);
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
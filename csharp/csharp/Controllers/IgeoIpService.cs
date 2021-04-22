using System.Threading.Tasks;

namespace csharp.Controllers
{
    public interface IgeoIpService
    {
        Task<string> GetMyip();
        Task<whereAmIRespons> GetMyGeoData(string myIp);
    }
}
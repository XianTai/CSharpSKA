using System.Threading.Tasks;

namespace csharp.Controllers
{
    public interface IgeoIpService
    {
        public  Task<whereAmIRespons> GetIpAndCountryCode();
    }
}
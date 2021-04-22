using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class whereAmIController : ControllerBase
    {
        private readonly IgeoIpService _geoIpService;
        public  whereAmIController(IgeoIpService geosGeoIpService)
        {
            _geoIpService = geosGeoIpService;
        }
        public async Task<IActionResult> Index()
        {
            var myip = await _geoIpService.GetMyip();
            var myGeoData = _geoIpService.GetMyGeoData(myip);

            return new JsonResult( myGeoData );
        }
    }
}

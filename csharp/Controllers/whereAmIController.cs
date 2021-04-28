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

        //public IgeoIpService GeoIpService
        //{
        //    get { return _geoIpService; }
        //}

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> Index()
        {
            var myGeoData = await _geoIpService.GetIpAndCountryCode();

            return new JsonResult( myGeoData );
        }
    }
}

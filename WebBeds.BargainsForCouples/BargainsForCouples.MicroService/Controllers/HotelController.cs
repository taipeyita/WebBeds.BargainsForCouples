using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BargainsForCouples.MicroService.Dto;
using BargainsForCouples.MicroService.Filters;
using BargainsForCouples.MicroService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BargainsForCouples.MicroService.Controllers
{
    [Route("api/[controller]")]
    public class HotelController : Controller
    {
        private readonly IBargainService _BargainService;

        public HotelController(IBargainService bargainService)
        {
            _BargainService = bargainService;
        }

        [HttpGet()]
        [Route("GetBargain")]
        [ProducesResponseType(typeof(Model.Property), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [CacheFilter(60)]
        public async Task<ActionResult<List<PropertyBO>>> GetBargainsAsync(int destinationId, int nights)
        {
            List<PropertyBO> hotels = await _BargainService.FindBargains(destinationId, nights);

            return hotels;
        }
    }
}
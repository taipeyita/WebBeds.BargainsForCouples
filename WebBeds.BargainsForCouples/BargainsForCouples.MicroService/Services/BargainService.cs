using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BargainsForCouples.MicroService.Dto;
using BargainsForCouples.MicroService.Model;
using Microsoft.Extensions.Logging;
using WebBeds.BargainsForCouples.Core.Helpers;

namespace BargainsForCouples.MicroService.Services
{
    /// <summary>
    /// Bargain Service that fetches bargains from the Bargains for More api client 
    /// </summary>
    public class BargainService : IBargainService
    {
      
        private readonly IBargainsForCouplesApiClient _Client;
        private readonly ILogger<BargainService> _Logger;
        private readonly IMapper _Mapper;

        public BargainService(IBargainsForCouplesApiClient client, ILogger<BargainService> logger, IMapper mapper)
        {
            _Client = client;
            _Logger = logger;
            _Mapper = mapper;
        }

        /// <summary>
        /// Gets bargains from the bargains for more api client
        /// </summary>
        /// <param name="destinationId"></param>
        /// <param name="nights"></param>
        /// <returns></returns>
        public async Task<List<PropertyBO>> FindBargains(int destinationId, int nights)
        {           
            List<Property> properties = await _Client.FindBargains(destinationId, nights);

            List<PropertyBO> propertiesBO = _Mapper.Map<List<PropertyBO>>(properties);

            propertiesBO.
                ForEach(p => p.Rates.
                ForEach(r => r.FinalPrice =
                RateHelper.Calculate(r.RateType, nights, r.FinalPrice)));
         
            return propertiesBO;
        }
    }
}

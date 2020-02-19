using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BargainsForCouples.MicroService.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BargainsForCouples.MicroService.Services
{
    /// <summary>
    /// Gets data from bargains for couples
    /// </summary>
    public class BargainsForCouplesApiClient : IBargainsForCouplesApiClient
    {
        private readonly HttpClient _HttpClient;
        private readonly BargainsForCouplesSettings _Settings;
   
        private readonly string _BaseUrl;

        public BargainsForCouplesApiClient(IOptions<BargainsForCouplesSettings> settings, HttpClient httpClient)
        {
            _HttpClient = httpClient;
            _Settings = settings.Value;           
            //_Logger = logger; TODO Add log functionality
            _BaseUrl = _Settings.BaseUrl;
        }

        /// <summary>
        /// Gets bargains 
        /// </summary>
        /// <param name="destinationId"></param>
        /// <param name="nights"></param>
        /// <returns></returns>
        public async Task<List<Property>> FindBargains(int destinationId, int nights)
        {
            try
            {
                List<Property> properties = null;
                var uri = $"{_BaseUrl}/findbargain?destinationId={destinationId}&nights={nights}&code={_Settings.ClientSecret}";

                var responseString = await _HttpClient.GetStringAsync(uri);

                 properties = JsonConvert.DeserializeObject<List<Property>>(responseString);

                return properties;
            }
            catch(Exception ex)
            {
                // Do no throw exception, rather log it
            }
            return new List<Property>();
        }
    }
}

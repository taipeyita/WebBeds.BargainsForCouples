using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BargainsForCouples.MicroService.Filters
{
    /// <summary>
    /// Attribute that caches request data. By doing this we can speed up the response to be less than 1 minute
    /// </summary>
    public class CacheFilter : TypeFilterAttribute
    {

        public CacheFilter() : base(typeof(CacheFilterInstance)) { }
       

        class CacheFilterInstance : IAsyncActionFilter
        {
            IDistributedCache _cache;

            public CacheFilterInstance(IDistributedCache cache)
            {
                this._cache = cache;
            }

            public async Task OnActionExecutionAsync(
                ActionExecutingContext context,
                ActionExecutionDelegate next)
            {

                var cacheKey = GetTheCacheKey(context);

                var cachedString = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedString))
                {
                    context.Result = new Microsoft.AspNetCore.Mvc.ObjectResult(cachedString);
                }
                else
                {
                    var resultContext = await next();

                    var result = resultContext.Result;
                    if (result != null)
                    {
                        var value = result.GetType().GetProperty("Value").GetValue(result, null);

                        if (value != null)
                        {
                            await SetTheCache(cacheKey, value);
                        }
                    }
                }
            }

            private async Task SetTheCache(string cacheKey, object value)
            {
                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };

                var cacheOptions = new DistributedCacheEntryOptions();
                cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(60)); // TODO -- Move this to appsettings.json
                var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = contractResolver };
                jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                var jsonString = JsonConvert.SerializeObject(value, jsonSerializerSettings);
                await _cache.SetStringAsync(cacheKey, jsonString, cacheOptions);
            }

            private string GetTheCacheKey(ActionExecutingContext context)
            {
                var cacheKey = context.HttpContext.Request.Path.Value.Replace("/", "-");
                return cacheKey;
            }
        }
    }
}

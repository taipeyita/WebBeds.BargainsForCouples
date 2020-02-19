using System.Collections.Generic;
using System.Threading.Tasks;
using BargainsForCouples.MicroService.Model;

namespace BargainsForCouples.MicroService.Services
{
    public interface IBargainsForCouplesApiClient
    {
        Task<List<Property>> FindBargains(int destinationId, int nights);
    }
}

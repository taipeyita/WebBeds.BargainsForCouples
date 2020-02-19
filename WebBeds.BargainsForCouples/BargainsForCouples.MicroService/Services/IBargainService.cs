using System.Collections.Generic;
using System.Threading.Tasks;
using BargainsForCouples.MicroService.Dto;

namespace BargainsForCouples.MicroService.Services
{
    public interface IBargainService
    {
        Task<List<PropertyBO>> FindBargains(int destinationId, int nights);
    }
}

using DealGeinieCrmService.Models;

namespace DealGeinieCrmService.Services
{
    public interface ICrmService
    {
        void ProcessEntity(ICrmEntity entity);
    }
}

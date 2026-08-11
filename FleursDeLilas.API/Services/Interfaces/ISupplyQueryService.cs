using FleursDeLilas.API.DTOs;

namespace FleursDeLilas.API.Services.Interfaces
{
    public interface ISupplyQueryService
    {
        Task<IEnumerable<SupplyDto>> GetAllAsync();
        Task<SupplyDto?> GetByIdAsync(int id);
    }
}

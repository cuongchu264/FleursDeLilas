using FleursDeLilas.API.DTOs;

namespace FleursDeLilas.API.Services.Interfaces
{
    public interface IFlowerQueryService
    {
        Task<IEnumerable<FlowerDto>> GetAllAsync();
        Task<FlowerDto?> GetByIdAsync(int id);
    }
}

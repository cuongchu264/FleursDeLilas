using FleursDeLilas.API.DTOs;

namespace FleursDeLilas.API.Services.Interfaces
{
    public interface IFleursUserQueryService
    {
        Task<IEnumerable<FleursUserDto>> GetAllAsync();
        Task<FleursUserDto?> GetByIdAsync(int id);
    }
}

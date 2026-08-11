using FleursDeLilas.API.DTOs;

namespace FleursDeLilas.API.Services.Interfaces
{
    public interface IFlowerCommandService
    {
        Task<FlowerDto> CreateAsync(CreateFlowerDto dto);
        Task<bool> UpdateAsync(int id, UpdateFlowerDto dto);
        Task<bool> DeleteByIdAsync(int id);
    }
}

using FleursDeLilas.API.DTOs;

namespace FleursDeLilas.API.Services.Interfaces
{
    public interface IFleursUserCommandService
    {
        Task<FleursUserDto> CreateAsync(CreateFleursUserDto dto);
        Task<bool> UpdateAsync(int id, UpdateFleursUserDto dto);
        Task<bool> DeleteByIdAsync(int id);
    }
}

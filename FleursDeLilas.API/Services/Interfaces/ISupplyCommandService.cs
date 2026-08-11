using FleursDeLilas.API.DTOs;

namespace FleursDeLilas.API.Services.Interfaces
{
    public interface ISupplyCommandService
    {
        Task<SupplyDto> CreateAsync(CreateSupplyDto dto);
        Task<bool> UpdateAsync(int id, UpdateSupplyDto dto);
        Task<bool> DeleteByIdAsync(int id);
    }
}

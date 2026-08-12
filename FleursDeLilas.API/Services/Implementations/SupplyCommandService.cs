using FleursDeLilas.API.DTOs;
using FleursDeLilas.API.Entities;
using FleursDeLilas.API.Repositories.Interfaces;
using FleursDeLilas.API.Services.Interfaces;

namespace FleursDeLilas.API.Services.Implementations
{
    public class SupplyCommandService : ISupplyCommandService
    {
        private readonly ISupplyRepository _supplyRepository;

        public SupplyCommandService(ISupplyRepository supplyRepository)
        {
            _supplyRepository = supplyRepository;
        }

        private static DateTime? NormalizeUtcDate(DateTime? value)
        {
            if (value == null)
                return null;

            if (value.Value.Kind == DateTimeKind.Utc)
                return value;

            if (value.Value.Kind == DateTimeKind.Unspecified)
                return DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);

            return value.Value.ToUniversalTime();
        }

        public async Task<SupplyDto> CreateAsync(CreateSupplyDto dto)
        {
            var entity = new Supply
            {
                Name = dto.Name,
                Price = dto.Price,
                Count = dto.Count,
                BuyDate = NormalizeUtcDate(dto.BuyDate),
                Note = dto.Note,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _supplyRepository.AddAsync(entity);
            await _supplyRepository.SaveChangesAsync();

            return new SupplyDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Count = entity.Count,
                Note = entity.Note,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateSupplyDto dto)
        {
            var entity = await _supplyRepository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.Count = dto.Count;
            entity.BuyDate = NormalizeUtcDate(dto.BuyDate);
            entity.Note = dto.Note;
            entity.UpdatedAt = DateTime.UtcNow;

            _supplyRepository.Update(entity);
            return await _supplyRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _supplyRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _supplyRepository.Delete(entity);
            return await _supplyRepository.SaveChangesAsync();
        }
    }
}

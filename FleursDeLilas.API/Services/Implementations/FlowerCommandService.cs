using FleursDeLilas.API.DTOs;
using FleursDeLilas.API.Entities;
using FleursDeLilas.API.Repositories.Interfaces;
using FleursDeLilas.API.Services.Interfaces;

namespace FleursDeLilas.API.Services.Implementations
{
    public class FlowerCommandService : IFlowerCommandService
    {
        private readonly IFlowerRepository _flowerRepository;

        public FlowerCommandService(IFlowerRepository flowerRepository)
        {
            _flowerRepository = flowerRepository;
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

        public async Task<FlowerDto> CreateAsync(CreateFlowerDto dto)
        {
            var entity = new Flower
            {
                Name = dto.Name,
                Price = dto.Price,
                TotalCount = dto.TotalCount,
                AvailableCount = dto.AvailableCount,
                FailedCount = dto.FailedCount,
                BuyDate = NormalizeUtcDate(dto.BuyDate),
                Note = dto.Note,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _flowerRepository.AddAsync(entity);
            await _flowerRepository.SaveChangesAsync();

            return new FlowerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                TotalCount = entity.TotalCount,
                AvailableCount = entity.AvailableCount,
                FailedCount = entity.FailedCount,
                BuyDate = entity.BuyDate,
                Note = entity.Note,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateFlowerDto dto)
        {
            var entity = await _flowerRepository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.TotalCount = dto.TotalCount;
            entity.AvailableCount = dto.AvailableCount;
            entity.FailedCount = dto.FailedCount;
            entity.BuyDate = NormalizeUtcDate(dto.BuyDate);
            entity.Note = dto.Note;
            entity.UpdatedAt = DateTime.UtcNow;

            _flowerRepository.Update(entity);
            return await _flowerRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _flowerRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _flowerRepository.Delete(entity);
            return await _flowerRepository.SaveChangesAsync();
        }
    }
}

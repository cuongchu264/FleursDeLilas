using FleursDeLilas.API.DTOs;
using FleursDeLilas.API.Entities;
using FleursDeLilas.API.Repositories.Interfaces;
using FleursDeLilas.API.Services.Interfaces;

namespace FleursDeLilas.API.Services.Implementations
{
    public class FleursUserCommandService : IFleursUserCommandService
    {
        private readonly IFleursUserRepository _fleursUserRepository;

        public FleursUserCommandService(IFleursUserRepository fleursUserRepository)
        {
            _fleursUserRepository = fleursUserRepository;
        }

        public async Task<FleursUserDto> CreateAsync(CreateFleursUserDto dto)
        {
            var entity = new FleursUser
            {
                Username = dto.Username,
                Password = dto.Password, // Note: Hash password before saving in production
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _fleursUserRepository.AddAsync(entity);
            await _fleursUserRepository.SaveChangesAsync();

            return new FleursUserDto
            {
                Id = entity.Id,
                Username = entity.Username,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateFleursUserDto dto)
        {
            var entity = await _fleursUserRepository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Username = dto.Username;
            entity.Password = dto.Password;
            entity.UpdatedAt = DateTime.UtcNow;

            _fleursUserRepository.Update(entity);
            return await _fleursUserRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _fleursUserRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _fleursUserRepository.Delete(entity);
            return await _fleursUserRepository.SaveChangesAsync();
        }
    }
}

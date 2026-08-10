using Dapper;
using FleursDeLilas.API.Data;
using FleursDeLilas.API.DTOs;
using FleursDeLilas.API.Services.Interfaces;

namespace FleursDeLilas.API.Services.Implementations
{
    public class FleursUserQueryService : IFleursUserQueryService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public FleursUserQueryService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<FleursUserDto>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            // Execute raw SQL using Dapper for optimal read performance
            const string sql = @"
                SELECT id, username, created_at, updated_at 
                FROM fleurs_user 
                ORDER BY id DESC;";

            return await connection.QueryAsync<FleursUserDto>(sql);
        }

        public async Task<FleursUserDto?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT id, username, created_at, updated_at 
                FROM fleurs_user 
                WHERE id = @Id;";

            return await connection.QueryFirstOrDefaultAsync<FleursUserDto>(sql, new { Id = id });
        }
    }
}

using Dapper;
using FleursDeLilas.API.Data;
using FleursDeLilas.API.DTOs;
using FleursDeLilas.API.Services.Interfaces;

namespace FleursDeLilas.API.Services.Implementations
{
    public class FlowerQueryService : IFlowerQueryService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public FlowerQueryService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<FlowerDto>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            // Execute raw SQL using Dapper for optimal read performance
            const string sql = @"
                SELECT id, flo_name AS name, flo_price AS price, flo_toal_count AS totalcount, 
                       flo_avaiable_count AS availablecount, flo_failed_count AS failedcount, 
                       flo_buy_date AS buydate, flo_note AS note, created_at AS createdat, updated_at AS updatedat
                FROM flower 
                ORDER BY flo_buy_date DESC NULLS LAST, id DESC;";

            return await connection.QueryAsync<FlowerDto>(sql);
        }

        public async Task<FlowerDto?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT id, flo_name AS name, flo_price AS price, flo_toal_count AS totalcount, 
                       flo_avaiable_count AS availablecount, flo_failed_count AS failedcount, 
                       flo_buy_date AS buydate, flo_note AS note, created_at AS createdat, updated_at AS updatedat
                FROM flower 
                WHERE id = @Id;";

            return await connection.QueryFirstOrDefaultAsync<FlowerDto>(sql, new { Id = id });
        }
    }
}

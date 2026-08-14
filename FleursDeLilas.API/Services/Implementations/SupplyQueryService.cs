using Dapper;
using FleursDeLilas.API.Data;
using FleursDeLilas.API.DTOs;
using FleursDeLilas.API.Services.Interfaces;

namespace FleursDeLilas.API.Services.Implementations
{
    public class SupplyQueryService : ISupplyQueryService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SupplyQueryService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<SupplyDto>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            // Execute raw SQL using Dapper for optimal read performance
                 const string sql = @"
                  SELECT id, sup_name AS name, sup_price AS price, sup_count AS count, sup_sold_count AS soldcount,
                      sup_buy_date AS buydate, sup_note AS note, created_at AS createdat, updated_at AS updatedat
                  FROM supply 
                  ORDER BY sup_buy_date DESC NULLS LAST, id DESC;";

            return await connection.QueryAsync<SupplyDto>(sql);
        }

        public async Task<SupplyDto?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

                 const string sql = @"
                  SELECT id, sup_name AS name, sup_price AS price, sup_count AS count, sup_sold_count AS soldcount,
                      sup_buy_date AS buydate, sup_note AS note, created_at AS createdat, updated_at AS updatedat
                  FROM supply 
                  WHERE id = @Id;";

            return await connection.QueryFirstOrDefaultAsync<SupplyDto>(sql, new { Id = id });
        }
    }
}

using Dapper;
using FleursDeLilas.API.Data;
using FleursDeLilas.API.DTOs;
using FleursDeLilas.API.Services.Interfaces;

namespace FleursDeLilas.API.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public OrderService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            // Validate and lock selected flower rows
            var flowerIds = dto.Flowers.Select(f => f.Id).ToArray();
            var supplyIds = dto.Supplies.Select(s => s.Id).ToArray();

            var flowerMap = new Dictionary<int, dynamic>();
            if (flowerIds.Any())
            {
                var sql = "SELECT id, flo_price, flo_toal_count, flo_avaiable_count, flo_sold_count FROM flower WHERE id = ANY(@Ids) FOR UPDATE;";
                var rows = await connection.QueryAsync(sql, new { Ids = flowerIds }, transaction);
                foreach (var r in rows) flowerMap[(int)r.id] = r;
            }

            var supplyMap = new Dictionary<int, dynamic>();
            if (supplyIds.Any())
            {
                var sql = "SELECT id, sup_price, sup_count, sup_sold_count FROM supply WHERE id = ANY(@Ids) FOR UPDATE;";
                var rows = await connection.QueryAsync(sql, new { Ids = supplyIds }, transaction);
                foreach (var r in rows) supplyMap[(int)r.id] = r;
            }

            // Compute totals and validate availability
            decimal flowerTotal = 0m;
            foreach (var f in dto.Flowers)
            {
                if (!flowerMap.ContainsKey(f.Id)) throw new Exception($"Flower id {f.Id} not found.");
                var row = flowerMap[f.Id];
                int available = (int)row.flo_avaiable_count;
                if (f.Qty > available) throw new Exception($"Requested qty for flower {f.Id} exceeds available ({available}).");
                decimal price = (decimal)row.flo_price;
                int totalCount = (int)row.flo_toal_count;
                decimal unitPrice = totalCount > 0 ? price / totalCount : 0m;
                flowerTotal += unitPrice * f.Qty;
            }

            decimal supplyTotal = 0m;
            foreach (var s in dto.Supplies)
            {
                if (!supplyMap.ContainsKey(s.Id)) throw new Exception($"Supply id {s.Id} not found.");
                var row = supplyMap[s.Id];
                int available = (int)row.sup_count;
                if (s.Qty > available) throw new Exception($"Requested qty for supply {s.Id} exceeds available ({available}).");
                decimal price = (decimal)row.sup_price;
                int totalCount = (int)row.sup_count;
                decimal unitPrice = totalCount > 0 ? price / totalCount : 0m;
                supplyTotal += unitPrice * s.Qty;
            }

            // Apply formula: bouquet = flowerTotal*3*1.3 + supplyTotal*2
            decimal bouquet = Math.Round(flowerTotal * 3m * 1.3m + supplyTotal * 2m, 2);

            // Insert order
            const string insertOrderSql = @"INSERT INTO fleurs_order (order_name, order_price, order_ship_price, order_date, created_at, updated_at)
                                            VALUES (@Name, @Price, @ShipPrice, NOW(), NOW(), NOW()) RETURNING id, order_name AS ordername, order_price, order_ship_price, order_date;";

            var order = await connection.QuerySingleAsync<OrderDto>(insertOrderSql, new { Name = dto.OrderName ?? "", Price = bouquet, ShipPrice = dto.OrderShipPrice ?? 0m }, transaction);

            // Insert order_prepare_flo and update sold counts
            const string insertPrepFlo = @"INSERT INTO order_prepare_flo (flo_id, order_id, order_pre_flo_count, created_at, updated_at)
                                           VALUES (@FloId, @OrderId, @Count, NOW(), NOW());";
            const string updateFloSold = @"UPDATE flower SET flo_sold_count = flo_sold_count + @Qty WHERE id = @Id;";

            foreach (var f in dto.Flowers)
            {
                await connection.ExecuteAsync(insertPrepFlo, new { FloId = f.Id, OrderId = order.Id, Count = f.Qty }, transaction);
                await connection.ExecuteAsync(updateFloSold, new { Qty = f.Qty, Id = f.Id }, transaction);
            }

            // Insert order_prepare_suplly and update sold counts
            const string insertPrepSup = @"INSERT INTO order_prepare_suplly (sup_id, order_id, order_pre_up_count, created_at, updated_at)
                                           VALUES (@SupId, @OrderId, @Count, NOW(), NOW());";
            const string updateSupSold = @"UPDATE supply SET sup_sold_count = sup_sold_count + @Qty WHERE id = @Id;";

            foreach (var s in dto.Supplies)
            {
                await connection.ExecuteAsync(insertPrepSup, new { SupId = s.Id, OrderId = order.Id, Count = s.Qty }, transaction);
                await connection.ExecuteAsync(updateSupSold, new { Qty = s.Qty, Id = s.Id }, transaction);
            }

            transaction.Commit();

            return order;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"SELECT id, order_name AS ordername, order_price, order_ship_price, order_date FROM fleurs_order ORDER BY order_date DESC, id DESC;";
            var orders = (await connection.QueryAsync<OrderDto>(sql)).ToArray();

            if (!orders.Any()) return orders;

            var ids = orders.Select(o => o.Id).ToArray();

            const string floSql = @"
                SELECT opf.order_id AS orderid, opf.id, opf.flo_id AS itemid, f.flo_name AS itemname, opf.order_pre_flo_count AS qty, f.flo_price AS price
                FROM order_prepare_flo opf
                JOIN flower f ON f.id = opf.flo_id
                WHERE opf.order_id = ANY(@Ids);
            ";

            const string supSql = @"
                SELECT ops.order_id AS orderid, ops.id, ops.sup_id AS itemid, s.sup_name AS itemname, ops.order_pre_up_count AS qty, s.sup_price AS price
                FROM order_prepare_suplly ops
                JOIN supply s ON s.id = ops.sup_id
                WHERE ops.order_id = ANY(@Ids);
            ";

            var floRows = (await connection.QueryAsync(floSql, new { Ids = ids })).ToArray();
            var supRows = (await connection.QueryAsync(supSql, new { Ids = ids })).ToArray();

            var floLookup = floRows.GroupBy(r => (int)r.orderid).ToDictionary(g => g.Key, g => g.Select(r => new OrderItemDto
            {
                Id = (int)r.id,
                ItemId = (int)r.itemid,
                ItemName = (string)r.itemname,
                Qty = (int)r.qty,
                Price = (decimal)r.price
            }).ToArray());

            var supLookup = supRows.GroupBy(r => (int)r.orderid).ToDictionary(g => g.Key, g => g.Select(r => new OrderItemDto
            {
                Id = (int)r.id,
                ItemId = (int)r.itemid,
                ItemName = (string)r.itemname,
                Qty = (int)r.qty,
                Price = (decimal)r.price
            }).ToArray());

            foreach (var o in orders)
            {
                if (floLookup.TryGetValue(o.Id, out var fitems)) o.Flowers = fitems;
                if (supLookup.TryGetValue(o.Id, out var sitems)) o.Supplies = sitems;
            }

            return orders;
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"SELECT id, order_name AS ordername, order_price, order_ship_price, order_date FROM fleurs_order WHERE id = @Id;";
            var order = await connection.QueryFirstOrDefaultAsync<OrderDto>(sql, new { Id = id });
            if (order == null) return null;

            const string floSql = @"
                SELECT opf.id, opf.flo_id AS itemid, f.flo_name AS itemname, opf.order_pre_flo_count AS qty, f.flo_price AS price
                FROM order_prepare_flo opf
                JOIN flower f ON f.id = opf.flo_id
                WHERE opf.order_id = @Id;";

            var supSql = @"
                SELECT ops.id, ops.sup_id AS itemid, s.sup_name AS itemname, ops.order_pre_up_count AS qty, s.sup_price AS price
                FROM order_prepare_suplly ops
                JOIN supply s ON s.id = ops.sup_id
                WHERE ops.order_id = @Id;";

            var floItems = (await connection.QueryAsync(floSql, new { Id = id })).Select(r => new OrderItemDto
            {
                Id = (int)r.id,
                ItemId = (int)r.itemid,
                ItemName = (string)r.itemname,
                Qty = (int)r.qty,
                Price = (decimal)r.price
            }).ToArray();

            var supItems = (await connection.QueryAsync(supSql, new { Id = id })).Select(r => new OrderItemDto
            {
                Id = (int)r.id,
                ItemId = (int)r.itemid,
                ItemName = (string)r.itemname,
                Qty = (int)r.qty,
                Price = (decimal)r.price
            }).ToArray();

            order.Flowers = floItems;
            order.Supplies = supItems;

            return order;
        }
    }
}

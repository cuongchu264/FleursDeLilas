using FleursDeLilas.API.Data;
using FleursDeLilas.API.Entities;
using FleursDeLilas.API.Repositories.Interfaces;

namespace FleursDeLilas.API.Repositories.Implementations
{
    public class SupplyRepository : GenericRepository<Supply>, ISupplyRepository
    {
        public SupplyRepository(AppDbContext context) : base(context)
        {
        }
    }
}

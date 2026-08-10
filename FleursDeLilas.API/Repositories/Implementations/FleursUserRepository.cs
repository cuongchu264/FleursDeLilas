using FleursDeLilas.API.Data;
using FleursDeLilas.API.Entities;
using FleursDeLilas.API.Repositories.Interfaces;

namespace FleursDeLilas.API.Repositories.Implementations
{
    public class FleursUserRepository : GenericRepository<FleursUser>, IFleursUserRepository
    {
        public FleursUserRepository(AppDbContext context) : base(context)
        {
        }
    }
}

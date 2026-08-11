using FleursDeLilas.API.Data;
using FleursDeLilas.API.Entities;
using FleursDeLilas.API.Repositories.Interfaces;

namespace FleursDeLilas.API.Repositories.Implementations
{
    public class FlowerRepository : GenericRepository<Flower>, IFlowerRepository
    {
        public FlowerRepository(AppDbContext context) : base(context)
        {
        }
    }
}

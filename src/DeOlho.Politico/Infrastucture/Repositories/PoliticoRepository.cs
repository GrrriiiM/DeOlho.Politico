using DeOlho.SeedWork.Infrastructure.Data;
using DeOlho.SeedWork.Infrastructure.Repositories;

namespace DeOlho.Politico.Infrastucture.Repositories
{
    public class PoliticoRepository : Repository<Domain.Politico>
    {
        public PoliticoRepository(DeOlhoDbContext deOlhoDbContext) : base(deOlhoDbContext)
        {
        }
    }
}
using Catalog.Api.Domain;
using Catalog.Api.Infrastructure.Data;
using System.Threading.Tasks;

namespace Catalog.Api.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductDbContext _context;

        public UnitOfWork(ProductDbContext context)
        {
            _context = context;
        }

        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
using System.Threading.Tasks;

namespace Catalog.Api.Domain
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
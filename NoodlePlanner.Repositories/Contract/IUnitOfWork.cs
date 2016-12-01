using System.Threading.Tasks;

namespace NoodlePlanner.Repositories.Contract
{
    public interface IUnitOfWork
    {
        void Dispose();
        void Save();
        Task SaveAsync();
        void Dispose(bool disposing);
        IRepository<T> Repository<T>() where T : class;
    }
}

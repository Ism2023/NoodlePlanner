using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace NoodlePlanner.DBContext.Contract
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Database Database { get; }
        DbEntityEntry ContextEntry(object o);
        DbChangeTracker GetChangeTracker();
        void Dispose();
    }
}

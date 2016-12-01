using NoodlePlanner.Common.Contract;
using NoodlePlanner.DBContext.Contract;
using NoodlePlanner.Repositories.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoodlePlanner.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbContext _ctx;
        private readonly IUserProfile _userProfile;

        private bool _disposed;
        private Hashtable _repos;

        public UnitOfWork(IDbContext ctx, IUserProfile userProfile)
        {
            _ctx = ctx;
            _userProfile = userProfile;
        }
        public void Save()
        {
            var changeTracker = _ctx.GetChangeTracker();

            var addedAuditedEntities = changeTracker.Entries<IAuditedEntity>()
                                         .Where(p => p.State == EntityState.Added)
                                         .Select(p => p.Entity);

            foreach (IAuditedEntity entity in addedAuditedEntities)
            {
                entity.Created = DateTime.UtcNow;
                entity.CreatedById = _userProfile.UserId;
                entity.LastUpdated = DateTime.UtcNow;
                entity.LastUpdatedById = _userProfile.UserId;
            }

            var modifiedAuditedEntities = changeTracker.Entries<IAuditedEntity>()
                  .Where(p => p.State == EntityState.Modified)
                  .Select(p => p.Entity);

            foreach (IAuditedEntity entity in modifiedAuditedEntities)
            {
                entity.LastUpdated = DateTime.UtcNow;
                entity.LastUpdatedById = _userProfile.UserId;
                entity.ModificationNumber = entity.ModificationNumber + 1;
            }

            _ctx.SaveChanges();
        }
        public async Task SaveAsync()
        {
            try
            {
                var changeTracker = _ctx.GetChangeTracker();

                var addedAuditedEntities = changeTracker.Entries<IAuditedEntity>()
                                             .Where(p => p.State == EntityState.Added)
                                             .Select(p => p.Entity);

                foreach (IAuditedEntity entity in addedAuditedEntities)
                {
                    entity.Created = DateTime.UtcNow;
                    entity.CreatedById = _userProfile.UserId;

                    entity.LastUpdated = DateTime.UtcNow;
                    entity.LastUpdatedById = _userProfile.UserId;
                }

                var modifiedAuditedEntities = changeTracker.Entries<IAuditedEntity>()
                      .Where(p => p.State == EntityState.Modified)
                      .Select(p => p.Entity);

                foreach (IAuditedEntity entity in modifiedAuditedEntities)
                {
                    entity.LastUpdated = DateTime.UtcNow;
                    entity.LastUpdatedById = _userProfile.UserId;
                    entity.ModificationNumber = entity.ModificationNumber + 1;
                }
                await _ctx.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IRepository<T> Repository<T>() where T : class
        {
            if (_repos == null)
                _repos = new Hashtable();

            var type = typeof(T).Name;

            if (!_repos.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), _ctx);

                _repos.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repos[type];
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _ctx.Dispose();

            _disposed = true;
        }
    }
}

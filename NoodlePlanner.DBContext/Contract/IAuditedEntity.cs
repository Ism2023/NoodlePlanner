using System;

namespace NoodlePlanner.DBContext.Contract
{
    public interface IAuditedEntity
    {
        DateTime Created { get; set; }
        Guid CreatedById { get; set; }
        DateTime LastUpdated { get; set; }
        Guid LastUpdatedById { get; set; }
        int ModificationNumber { get; set; }
    }
}

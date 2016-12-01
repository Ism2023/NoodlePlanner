using NoodlePlanner.DBContext.Contract;
using System;
using System.ComponentModel.DataAnnotations;

namespace NoodlePlanner.DBContext.Entity
{
    public class Base : IAuditedEntity
    {
        [Key]
        public int UID { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        public Guid CreatedById { get; set; }
        public DateTime LastUpdated { get; set; }
        [Required]
        public Guid LastUpdatedById { get; set; }
        public int ModificationNumber { get; set; } = 0;
        public DateTime? Deleted { get; set; }
        public Guid? DeletedById { get; set; }
    }
}

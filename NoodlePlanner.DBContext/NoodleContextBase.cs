using System.Data.Entity;

namespace NoodlePlanner.DBContext
{
    public partial class NoodleContextBase : DbContext
    {
        public NoodleContextBase(string contextName)
            : base(contextName) { }
        public NoodleContextBase()
            : base ("NoodlePlanner") { }

        #region Tables

        #endregion

        protected override void OnModelCreating(DbModelBuilder mb)
        {
        }
    }
}

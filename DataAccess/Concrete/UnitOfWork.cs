using DataAccess.Abstract;
using DataAccess.Database;

namespace DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Members
        private readonly MasterContext _context;
        #endregion

        #region Constructors
        public UnitOfWork(MasterContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task<bool> Save()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                await _context.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }
}

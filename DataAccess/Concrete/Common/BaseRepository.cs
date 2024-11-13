using Data.Entities.Common;
using DataAccess.Abstract.Common;
using DataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Common
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        #region Members
        protected readonly MasterContext _context;
        private readonly DbSet<T> _dbSet;
        #endregion

        #region Constructor
        public BaseRepository(MasterContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        #endregion

        #region Methods
        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            return false;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

    }
    #endregion

}


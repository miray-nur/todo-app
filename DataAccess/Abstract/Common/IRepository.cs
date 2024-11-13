using Data.Entities.Common;

namespace DataAccess.Abstract.Common
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task Add(T Category);
        Task Update(T Category);
        Task<bool> Delete(int id);
        Task<List<T>> GetAll();

    }
}

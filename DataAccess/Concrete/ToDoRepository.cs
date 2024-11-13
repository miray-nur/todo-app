using Data.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.Common;
using DataAccess.Database;

namespace DataAccess.Concrete
{
    public class ToDoRepository : BaseRepository<ToDo>, IToDoRepository
    {
        public ToDoRepository(MasterContext context) : base(context)
        {
        }

        public async Task<ToDo> GetById(int id)
        {
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null)
            {
                throw new Exception("Todo bulunamadı.");
            }
            return toDo;
        }
    }
}

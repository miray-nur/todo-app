using Data.Entities;
using DataAccess.Abstract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IToDoRepository : IRepository<ToDo>
    {
        Task<ToDo> GetById(int id);
    }
}

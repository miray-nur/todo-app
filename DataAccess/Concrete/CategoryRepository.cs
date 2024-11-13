using Data.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.Common;
using DataAccess.Database;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MasterContext context) : base(context)
        {
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Kategori bulunamadı.");
            }
            return category;
        }
    }

}

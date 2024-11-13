using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class Dependencies
    {
        public static void AddDataAccessDependencies(this IServiceCollection collections) 
        {
            collections.AddScoped<ICategoryRepository, CategoryRepository>();
            collections.AddScoped<IToDoRepository, ToDoRepository>();
            collections.AddScoped<IUserRepository, UserRepository>();
            collections.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

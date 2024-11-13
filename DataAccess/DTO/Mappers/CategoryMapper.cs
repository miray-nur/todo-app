using Data.Entities;
using DataAccess.DTO.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Mappers
{
    public static class CategoryMapper
    {
        public static Category MapToEntity(CreateCategoryDto dto)
        {
            return new Category
            {
                Name = dto.Name,
                Description = dto.Description,
            };
        }

    }
}

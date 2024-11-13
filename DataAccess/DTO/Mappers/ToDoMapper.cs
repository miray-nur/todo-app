using Data.Entities;
using DataAccess.DTO.CategoryDtos;
using DataAccess.DTO.ToDoDtos;
using DataAccess.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Mappers
{
    public class ToDoMapper
    {
        public static ToDo MapToEntity(CreateToDoDto dto)
        {
            return new ToDo
            {
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                UserId = dto.UserId,
                Status = dto.Status,
                Priority = dto.Priority,
            };
        }

    }
}

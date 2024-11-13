using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.ToDoDtos
{
    public class CreateToDoDto
    {
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public ToDoStatus Status { get; set; }
        public ToDoPriority Priority { get; set; }
    }
}

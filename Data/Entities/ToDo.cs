using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Data.Entities.Common;
using Data.Enums;

namespace Data.Entities
{
    public class ToDo: BaseEntity
    {
        public string Description { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public  int CategoryId { get; set; }


        public User User { get; set; }
        public Category Category { get; set; }
    }
}

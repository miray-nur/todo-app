using Data.Enums;

namespace DataAccess.DTO.ToDoDtos
{
    public class UpdateToDoDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority Priority { get; set; }
    }
}

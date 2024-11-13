using Data.Entities.Common;

namespace Data.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<ToDo> ToDos { get; set; }
    }
}

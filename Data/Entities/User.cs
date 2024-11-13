using Data.Entities.Common;
using Data.Enums;

namespace Data.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Password { get; set; }

        public string Email { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<ToDo> ToDos { get; set; }
    }
}

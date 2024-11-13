using Data.Entities.Common;

namespace Data.Entities
{
    public class Audits : BaseEntity
    {
        public string Type { get; set; }
        public string TableName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }

        //Audit.Net kullanımı için gereklidir.
        //public string Data { get; set; }
    }
}

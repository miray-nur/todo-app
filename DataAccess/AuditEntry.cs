using Data.Entities;
using Data.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace DataAccess
{
    public class AuditEntry
    {
        //Audit.Net kullanımı için bu class diasble olmalı
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public AuditType AuditType { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();


        public Audits ToAudit()
        {
            var audit = new Audits();
            audit.Type = AuditType.ToString();
            audit.TableName = TableName;
            audit.CreatedDate = DateTime.Now;
            audit.OldValues = OldValues.Count == 0 ? "null" : JsonSerializer.Serialize(OldValues);
            audit.NewValues = NewValues.Count == 0 ? "null" : JsonSerializer.Serialize(NewValues);
            return audit;
        }
    }
}

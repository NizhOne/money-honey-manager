using System;

namespace API.Models.Domain
{
    public class Operation:BaseEntity
    {
        public virtual ApplicationUser Creator { get; set; }

        public string CreatorId { get; set; }

        public virtual Category Category { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Amount  { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }
    }
}

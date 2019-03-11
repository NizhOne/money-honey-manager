using API.Models.Domain;
using System;

namespace API.Models.Dto
{
    public class OperationDto: BaseEntity
    {
        public string CreatorId { get; set; }

        public CategoryDto Category { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }
    }
}

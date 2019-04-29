using API.Constants;
using API.Models.Domain;

namespace API.Models.Dto
{
    public class CategoryDto: BaseEntity
    {
        public string CreatorId { get; set; }

        public string Name { get; set; }

        public bool IsStandart { get; set; }

        public CategoryType Type { get; set; }
    }
}

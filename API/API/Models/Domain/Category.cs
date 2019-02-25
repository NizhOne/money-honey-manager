namespace API.Models.Domain
{
    public class Category: BaseEntity
    {
        public virtual ApplicationUser Creator { get; set; }

        public string CreatorId { get; set; }

        public string Name { get; set; }

        public bool IsStandart { get; set; }

        public byte Type { get; set; }
    }
}

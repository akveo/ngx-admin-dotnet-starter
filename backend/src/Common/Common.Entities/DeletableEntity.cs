namespace Common.Entities
{
    public abstract class DeletableEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }
    }
}
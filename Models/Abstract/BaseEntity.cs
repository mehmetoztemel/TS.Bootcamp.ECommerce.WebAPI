namespace TS.Bootcamp.ECommerce.WebAPI.Models.Abstract
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}

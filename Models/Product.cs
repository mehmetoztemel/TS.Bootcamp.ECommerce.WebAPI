using TS.Bootcamp.ECommerce.WebAPI.Models.Abstract;

namespace TS.Bootcamp.ECommerce.WebAPI.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}

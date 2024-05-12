using TS.Bootcamp.ECommerce.WebAPI.Models.Abstract;

namespace TS.Bootcamp.ECommerce.WebAPI.Models
{
    public class ShoppingCart : BaseEntity
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
    }
}

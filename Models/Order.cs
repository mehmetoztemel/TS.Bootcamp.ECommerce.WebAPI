using TS.Bootcamp.ECommerce.WebAPI.Models.Abstract;

namespace TS.Bootcamp.ECommerce.WebAPI.Models
{
    public class Order : BaseEntity
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
    }
}

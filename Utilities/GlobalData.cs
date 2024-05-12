using TS.Bootcamp.ECommerce.WebAPI.Models;

namespace TS.Bootcamp.ECommerce.WebAPI.Utilities
{
    public static class GlobalData
    {
        public static List<AppUser> Users = new List<AppUser>();
        public static List<Product> Products = new List<Product>();
        public static List<ShoppingCart> ShoppingCarts = new List<ShoppingCart>();
        public static List<Order> Orders = new List<Order>();
    }
}

namespace Bulky.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public List<ShoppingCart> shoppingCartList { get; set;}
        public OrderHeader OrderHeader { get; set;}
        public OrderDetail OrderDetail { get; set;}
    }
}
namespace e_commerce_system.Models.DTO
{
    public class CartItemOutDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string imageUrl { get; set; } = string.Empty;

        public decimal TotalPrice => Quantity * UnitPrice; // Calculate total price based on quantity and unit price

        public CartItemOutDTO()
        {
        }

        public CartItemOutDTO(CartItem cartItem)
        {
            ProductId = cartItem.ProductId;
            Quantity = cartItem.Quantity;
            ProductName = cartItem.product?.Name ?? string.Empty;
            UnitPrice = cartItem.UnitPrice;
            imageUrl = cartItem.product?.Images.FirstOrDefault()?.ImagePath ?? string.Empty;
        }

        public static CartItemOutDTO FromCartItem(CartItem cartItem)
        {
            return new CartItemOutDTO(cartItem);
        }   
    }
}

namespace e_commerce_system.Models.DTO

{
    public class CartOutputDTO
    {
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemOutDTO> CartItems { get; set; } = new List<CartItemOutDTO>();

        public CartOutputDTO()
        {
        }


        public CartOutputDTO(Cart cart)
        {
            TotalItems = cart.Items.Sum(item => item.Quantity);
            TotalPrice = cart.Items.Sum(item => item.Quantity * item.UnitPrice);
            CartItems = cart.Items.Select(item => CartItemOutDTO.FromCartItem(item)).ToList();
           
        }

        public static CartOutputDTO FromCart(Cart cart)
        {
            return new CartOutputDTO(cart);
        }

    }
}
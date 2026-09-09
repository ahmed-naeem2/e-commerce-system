namespace e_commerce_system.IServices
{
    public interface IOrderService
    {
        Task<> CheckoutAsync(Guid userId);

    }
}
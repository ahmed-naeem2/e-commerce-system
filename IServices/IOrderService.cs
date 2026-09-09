using e_commerce_system.Models;
using e_commerce_system.Models.DTO;
namespace e_commerce_system.IServices
{
    public interface IOrderService
    {
        Task<OrderOutputDTO?> CheckoutAsync(Guid userId);
        string GenerateOrderNumber(string orderNumber);
        void AddOrder(Order order);
        	Task SaveChangesAsync();

    }
}
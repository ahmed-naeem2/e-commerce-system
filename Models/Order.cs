using e_commerce_system.Enum;
using e_commerce_system.Models.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace e_commerce_system.Models
{
	public class Order
	{
		public Guid ID { get; set; }

		public string OrderNumber {  get; set; }

		public double TotalAmount { get; set; }

		public double SubTotal {  get; set; }

		public double ShippingAmount { get; set; }

		public string PaymentMethod { get; set; }

		public DateTime CreatedAt { get; set; }=DateTime.Now;

		public DateTime UpdatedAt { get; set; } 

		public OrderStatus Status { get; set; }=OrderStatus.PendingPayment;

		public Guid UserId { get; set; }

		public User? user { get; set; }

		public ICollection<OrderItem> Items { get; set; }=new List<OrderItem>();



	}
}

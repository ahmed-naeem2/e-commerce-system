namespace e_commerce_system.Models.DTO
{
	public class ProductUpdateDTO
	{
		public string? Name { get; set; }

		public string? Description { get; set; }



		public string? CategorieName { get; set; }

		public decimal? Price { get; set; }

		public bool IsActive { get; set; } = true;

	}
}

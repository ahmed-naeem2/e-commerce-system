namespace e_commerce_system.QueryFilter
{
	public class ProductQueryFilter
	{

		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;	
		public string? Search {  get; set; }

		public string? FilterByCatogrie { get; set; }

		public String ? SortBy { get; set; }




	}
}

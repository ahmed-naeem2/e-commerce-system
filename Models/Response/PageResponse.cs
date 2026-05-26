using System.Security.Cryptography.Pkcs;

namespace e_commerce_system.Models.Response
{
	public class PageResponse <T>
	{
		public IReadOnlyList< T> Data { get; init; }

		public int PageNumber {  get; init; }
		public int PageSize { get; init; }

		public int TotalPages {  get; init; }
		public int TotalRecords { get; init; }


	}
}

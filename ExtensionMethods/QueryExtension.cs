using System.Runtime.CompilerServices;

namespace e_commerce_system.ExtensionMethods
{
	public static class QueryExtension
	{

		public static IQueryable<T> ApplayPagination<T>(this IQueryable<T> query, int pageNumber, int pageSize) 
		{
		return	query.Skip((pageNumber-1)*pageSize)
				.Take(pageSize);
				


		}

	}
}

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http.HttpResults;

namespace e_commerce_system.ExtensionMethods
{
	public static class QueryExtension
	{

		public static IQueryable<T> ApplayPagination<T>(this IQueryable<T> query, int pageNumber, int pageSize) where T : class
		{
		return	query.Skip((pageNumber-1)*pageSize)
				.Take(pageSize);
				


		}

		public static IQueryable<T> ApplaySort <T>(this IQueryable<T> query,string? SortBy)where T : class
		{
			if(string .IsNullOrEmpty(SortBy))

					return query;

			var allwedPropeties = typeof(T)
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Select(p => p.Name)
				.ToHashSet(StringComparer.OrdinalIgnoreCase);
			var sortExpressions = new List<string>();
			foreach(var part in SortBy.Split(",",StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
			{

				var tokens = part.Split(" ", StringSplitOptions.RemoveEmptyEntries);
				if( tokens.Length==0 ||!allwedPropeties.Contains(tokens[0]))
					continue;

				var direction = tokens.Length > 1 && tokens[1].Equals("desc", StringComparison.OrdinalIgnoreCase) ?
					"descending"
					: "ascending";

				sortExpressions.Add($"{tokens[0]} {direction}");
			}
			return sortExpressions.Count > 0
		? query.OrderBy(string.Join(", ", sortExpressions))
		: query;
		}


		
	}
}

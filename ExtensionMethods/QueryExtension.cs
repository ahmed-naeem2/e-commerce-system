using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using e_commerce_system.Models;

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
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)//this do reflaction to properties of the class (T) BindingFlags.public GetAll Field Public.BindiniFlags.Instance don't get static properties 
				
				.Select(p => p.Name)
				.ToHashSet(StringComparer.OrdinalIgnoreCase);//convert To list of Hash Set Unique List 
			var sortExpressions = new List<string>();
			foreach(var part in SortBy.Split(",",StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
			{

				var tokens = part.Split(" ", StringSplitOptions.RemoveEmptyEntries);
				if( tokens.Length==0 || !allwedPropeties.Contains(tokens[0]) ||  tokens[0].ToLower()=="id")
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


		public static IQueryable<Product> ApplaySearch (this IQueryable<Product>query ,string Search)
		{
			if(string.IsNullOrEmpty(Search)) 
				return query;
			return query.Where(p =>
			p.Name.Contains(Search));


		}

		public static IQueryable<Product>ApplayFilterByCategorie(this IQueryable<Product>query,Guid Categorid)
		{

			if (Categorid==Guid.Empty)
				return query;


			return query.Where(p =>
			p.CategorieId == Categorid);

		}
	}
}

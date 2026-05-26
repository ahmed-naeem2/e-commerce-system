using e_commerce_system.Context;
using e_commerce_system.ExtensionMethods;
using e_commerce_system.Models.DTO;
using e_commerce_system.Models.Response;
using e_commerce_system.QueryFilter;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_system.Services
{
	public class PaginationService
	{
		private readonly MainAppDbContet _mainAppDbContet;
		private readonly CategorieService _categorieService;

		public PaginationService(MainAppDbContet mainAppDbContet, CategorieService categorieService)
		{
			_mainAppDbContet = mainAppDbContet;
			_categorieService = categorieService;
		}

		public async Task <PageResponse<ProductOutputDTO>>GetAllProduct(ProductQueryFilter filter ,CancellationToken token = default)
		{

			var pageNumber = Math.Max(1, filter.PageNumber);
			var pageSize = Math.Clamp(filter.PageSize, 1, 50);

			var query=_mainAppDbContet.Products.AsNoTracking().AsQueryable();

			query = query.ApplaySearch(filter.Search);

			var totalRecord = await query.CountAsync(token);

			query = query.ApplaySort(string.IsNullOrEmpty( filter.SortBy)?"Name":filter.SortBy) ;

			var Categorie =await _categorieService.GetCategorieByNameAsync(filter.FilterByCatogrie);

			if (Categorie is not null) {

				query = query.ApplayFilterByCategorie(Categorie.ID);
					}

			var products = await query
				.ApplayPagination(pageNumber, pageSize)
				.Select(p=>ProductOutputDTO.FromProduct(p))
				.ToListAsync(token);

			return new PageResponse<ProductOutputDTO>
			{
				Data = products,
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalRecords = totalRecord,
				TotalPages = (int)Math.Ceiling(totalRecord / (double)pageSize)


			};

		}
	}
}

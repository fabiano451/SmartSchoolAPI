using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.API.Helpers
{
	public class PageList<T> : List<T>
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }

		public PageList(List<T> items, int count, int pageNumber, int pageSize)
		{
			TotalCount = count;
			PageSize = pageSize;
			CurrentPage = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize); // ceiling para arredondar quantidade de pagina exe.
			                                                          // total count = 13  paginasize = 5
																	  // 13/5 = 2.6 ai arredonda para 3 assim não perde informações
			this.AddRange(items);
		}

		public static async Task<PageList<T>> CreateAsync(
			IQueryable<T> source, int pageNumber, int pageSize)
		{
			var count = await source.CountAsync();
			var items = await source.Skip((pageNumber - 1) * pageSize) // primeiro pula a quantidade de paginas ela começa na posição 0
									.Take(pageSize) // ele pega de acordo com o tamanho de paginas quantos itens
									.ToListAsync();
			return new PageList<T>(items, count, pageNumber, pageSize);
		}
	}
}


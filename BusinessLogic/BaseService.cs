namespace Forum.BusinessLogic
{
	using System.Linq;
	using System.Threading.Tasks;

	public class BaseService
	{
		private readonly IExtensionMethodsWrapper _extensionMethodsWrapper;

		protected BaseService(IExtensionMethodsWrapper extensionMethodsWrapper)
		{
			_extensionMethodsWrapper = extensionMethodsWrapper;
		}

		protected async Task<QueryResponse<T>> ToPagedQueryResponse<T>(IQueryable<T> query, Paging paging)
		{
			var count = await _extensionMethodsWrapper.CountAsync(query);

			paging.FixPageForCount(count);

			return new QueryResponse<T>
			{
				Total = count,
				Page = paging.Page,
				PageSize = paging.PageSizeValue,
				List = await _extensionMethodsWrapper.ToListAsync(query.ApplyPaging(paging))
			};
		}
	}
}
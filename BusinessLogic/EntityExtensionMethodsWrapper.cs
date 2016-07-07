namespace Forum.BusinessLogic
{
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Threading.Tasks;

	public class EntityExtensionMethodsWrapper : IExtensionMethodsWrapper
	{
		public Task<int> CountAsync<T>(IQueryable<T> source)
		{
			return source.CountAsync();
		}

		public Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> source)
		{
			return source.ToListAsync();
		}
	}
}
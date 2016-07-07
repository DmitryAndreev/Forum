namespace Forum.BusinessLogic
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public interface IExtensionMethodsWrapper
	{
		Task<int> CountAsync<T>(IQueryable<T> source);

		Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> source);
	}
}
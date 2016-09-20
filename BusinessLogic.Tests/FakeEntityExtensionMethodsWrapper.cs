namespace Forum.BusinessLogic.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class FakeEntityExtensionMethodsWrapper : IExtensionMethodsWrapper
	{
		public Task<int> CountAsync<T>(IQueryable<T> source)
		{
			return Task.FromResult(source.Count());
		}

		public Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> source)
		{
			return Task.FromResult(source.ToList());
		}
	}
}
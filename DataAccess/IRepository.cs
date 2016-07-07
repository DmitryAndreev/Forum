namespace Forum.DataAccess
{
	using System.Linq;
	using System.Threading.Tasks;

	public interface IRepository<T> where T : class
	{
		IQueryable<T> All();

		Task<T> FindById(object id);

		void Add(T entity);

		void Update(T entity);

		void Remove(T entity);
	}
}
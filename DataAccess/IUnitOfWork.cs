using Forum.Domain;

namespace Forum.DataAccess
{
	using System.Threading.Tasks;

	public interface IUnitOfWork
	{
		IRepository<Card> CardRepository { get; }

		Task Commit();
	}
}
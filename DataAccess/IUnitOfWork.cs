namespace Forum.DataAccess
{
	using System.Threading.Tasks;
	using Domain;

	public interface IUnitOfWork
	{
		IRepository<Message> MessageRepository { get; }

		Task Commit();
	}
}
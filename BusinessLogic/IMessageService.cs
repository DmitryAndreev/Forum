namespace Forum.BusinessLogic
{
	using System;
	using System.Threading.Tasks;
	using Domain;

	public interface IMessageService
	{
		Task<QueryResponse<Message>> Get(BaseQuery request);

		Task<Message> Get(Guid id);

		Task<Message> Create(Message item);

		Task<Message> Update(Message item);

		Task Delete(Guid id);
	}
}
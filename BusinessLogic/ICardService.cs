using Forum.Domain;

namespace Forum.BusinessLogic
{
	using System;
	using System.Threading.Tasks;

	public interface ICardService
	{
		Task<QueryResponse<Card>> Get(BaseQuery request);

		Task<Card> Get(Guid id);

		Task<Card> Create(Card item);

		Task<Card> Update(Card item);

		Task Delete(Guid id);
	}
}
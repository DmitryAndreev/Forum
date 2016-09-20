using Forum.Domain;

namespace Forum.BusinessLogic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Dynamic;
	using System.Threading.Tasks;
	using DataAccess;

	public class CardService : BaseService, ICardService
	{
		private readonly IUnitOfWork _uow;

		private readonly IExtensionMethodsWrapper _extensionMethodsWrapper;

		public CardService(IUnitOfWork uow, IExtensionMethodsWrapper extensionMethodsWrapper) : base(extensionMethodsWrapper)
		{
			_uow = uow;
			_extensionMethodsWrapper = extensionMethodsWrapper;
		}

		public async Task<QueryResponse<Card>> Get(BaseQuery request)
		{
			var query = _uow.CardRepository.All();
			query = ApplyOrderBy(query, request.Sortings);
			return await ToPagedQueryResponse(query, request.Paging);
		}

		public Task<Card> Get(Guid id)
		{
			return _uow.CardRepository.FindById(id);
		}

		public async Task<Card> Create(Card item)
		{
			_uow.CardRepository.Add(item);
			await _uow.Commit();
			return item;
		}

		public async Task<Card> Update(Card item)
		{
			_uow.CardRepository.Update(item);
			await _uow.Commit();
			return item;
		}

		public async Task Delete(Guid id)
		{
			var item = await _uow.CardRepository.FindById(id);
			if (item != null)
			{
				_uow.CardRepository.Remove(item);
				await _uow.Commit();
			}
		}

		private IQueryable<Card> ApplyOrderBy(IQueryable<Card> query, List<Sorting> sortings)
		{
			if (sortings == null || sortings.Count == 0)
			{
				return query.OrderBy(i => i.Id);
			}

			return query.OrderBy(string.Join(",", sortings));
		}
	}
}
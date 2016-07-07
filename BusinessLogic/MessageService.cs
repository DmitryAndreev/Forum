namespace Forum.BusinessLogic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Dynamic;
	using System.Threading.Tasks;
	using DataAccess;
	using Domain;

	public class MessageService : BaseService, IMessageService
	{
		private readonly IUnitOfWork _uow;

		private readonly IExtensionMethodsWrapper _extensionMethodsWrapper;

		public MessageService(IUnitOfWork uow, IExtensionMethodsWrapper extensionMethodsWrapper) : base(extensionMethodsWrapper)
		{
			_uow = uow;
			_extensionMethodsWrapper = extensionMethodsWrapper;
		}

		public async Task<QueryResponse<Message>> Get(BaseQuery request)
		{
			var query = _uow.MessageRepository.All();
			query = ApplyOrderBy(query, request.Sortings);
			return await ToPagedQueryResponse(query, request.Paging);
		}

		public Task<Message> Get(Guid id)
		{
			return _uow.MessageRepository.FindById(id);
		}

		public async Task<Message> Create(Message item)
		{
			_uow.MessageRepository.Add(item);
			await _uow.Commit();
			return item;
		}

		public async Task<Message> Update(Message item)
		{
			_uow.MessageRepository.Update(item);
			await _uow.Commit();
			return item;
		}

		public async Task Delete(Guid id)
		{
			var item =  await _uow.MessageRepository.FindById(id);
			if (item != null)
			{
				_uow.MessageRepository.Remove(item);
				await _uow.Commit();
			}
		}

		private IQueryable<Message> ApplyOrderBy(IQueryable<Message> query, List<Sorting> sortings)
		{
			if (sortings == null || sortings.Count == 0)
			{
				return query.OrderBy(i => i.Id);
			}

			return query.OrderBy(string.Join(",", sortings));
		}
	}
}
namespace Forum.DataAccess
{
	using System;
	using System.Data.Entity;
	using System.Threading.Tasks;
	using Domain;
	using JetBrains.Annotations;

	[UsedImplicitly]
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _context;
		private IRepository<Message> _itemRepository;

		public UnitOfWork(DbContext context)
		{
			_context = context;
		}

		public IRepository<Message> MessageRepository => _itemRepository ?? (_itemRepository = new EntityRepository<Message>(_context));

		public async Task Commit()
		{
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				//TODO: add log here
				throw;
			}
		}
	}
}
using Forum.Domain;

namespace Forum.DataAccess
{
	using System;
	using System.Data.Entity;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	[UsedImplicitly]
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _context;
		private IRepository<Card> _itemRepository;

		public UnitOfWork(DbContext context)
		{
			_context = context;
		}

		public IRepository<Card> CardRepository
		{
			get { return _itemRepository ?? (_itemRepository = new EntityRepository<Card>(_context)); }
		}

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
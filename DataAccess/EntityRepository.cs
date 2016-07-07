namespace Forum.DataAccess
{
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using System.Linq;
	using System.Threading.Tasks;

	public class EntityRepository<T> : IRepository<T>
		where T : class, new()
	{
		private readonly DbContext _context;

		private readonly DbSet<T> _dbSet;

		public EntityRepository(DbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public virtual IQueryable<T> All()
		{
			return _dbSet;
		}


		public virtual async Task<T> FindById(object id)
		{
			return await _dbSet.FindAsync(id);
		}

		public virtual void Add(T entity)
		{
			DbEntityEntry dbEntityEntry = _context.Entry(entity);
			if (dbEntityEntry.State != EntityState.Detached)
			{
				dbEntityEntry.State = EntityState.Added;
			}
			else
			{
				_dbSet.Add(entity);
			}
		}

		public virtual void Update(T entity)
		{
			DbEntityEntry dbEntityEntry = _context.Entry(entity);
			if (dbEntityEntry.State == EntityState.Detached)
			{
				_dbSet.Attach(entity);
			}
			dbEntityEntry.State = EntityState.Modified;
		}

		public virtual void Remove(T entity)
		{
			DbEntityEntry dbEntityEntry = _context.Entry(entity);
			if (dbEntityEntry.State != EntityState.Deleted)
			{
				dbEntityEntry.State = EntityState.Deleted;
			}
			else
			{
				_dbSet.Attach(entity);
				_dbSet.Remove(entity);
			}
		}
	}
}
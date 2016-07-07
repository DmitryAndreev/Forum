namespace Forum.DataAccess
{
	using System.Data.Entity;
	using JetBrains.Annotations;
	using Mappings;

	[UsedImplicitly]
	public class ForumDbContext : DbContext
	{
		public ForumDbContext() : base("DefaultConnection")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new MessageMap());

			base.OnModelCreating(modelBuilder);
		}
	}
}
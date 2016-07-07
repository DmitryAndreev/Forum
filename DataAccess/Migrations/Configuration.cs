namespace Forum.DataAccess.Migrations
{
	using System.Data.Entity.Migrations;
	using JetBrains.Annotations;

	[UsedImplicitly]
	public sealed class Configuration : DbMigrationsConfiguration<ForumDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ForumDbContext context)
        {
           
        }
    }
}

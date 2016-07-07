namespace Forum.DataAccess.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class Initial : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Messages",
				c => new
				{
					Id = c.Guid(false, true),
					Header = c.String(false, 250),
					Body = c.String(false)
				})
				.PrimaryKey(t => t.Id);
		}

		public override void Down()
		{
			DropTable("dbo.Messages");
		}
	}
}

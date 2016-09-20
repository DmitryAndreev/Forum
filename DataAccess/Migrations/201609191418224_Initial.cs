namespace Forum.DataAccess.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class Initial : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Cards",
				c => new
				{
					Id = c.Guid(nullable: false, identity: true),
					Description = c.String(nullable: false, maxLength: 250),
					Name = c.String(nullable: false, maxLength: 50),
					Link = c.String(nullable: false, maxLength: 1000),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.Categories",
				c => new
				{
					Id = c.Guid(nullable: false, identity: true),
					Name = c.String(),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.CardsCategories",
				c => new
				{
					CardId = c.Guid(nullable: false),
					CategoryId = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new {t.CardId, t.CategoryId})
				.ForeignKey("dbo.Cards", t => t.CardId, cascadeDelete: true)
				.ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
				.Index(t => t.CardId)
				.Index(t => t.CategoryId);
		}

		public override void Down()
		{
			DropForeignKey("dbo.CardsCategories", "CategoryId", "dbo.Categories");
			DropForeignKey("dbo.CardsCategories", "CardId", "dbo.Cards");
			DropIndex("dbo.CardsCategories", new[] {"CategoryId"});
			DropIndex("dbo.CardsCategories", new[] {"CardId"});
			DropTable("dbo.CardsCategories");
			DropTable("dbo.Categories");
			DropTable("dbo.Cards");
		}
	}
}
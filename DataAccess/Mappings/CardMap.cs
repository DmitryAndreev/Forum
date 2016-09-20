using Forum.Domain;

namespace Forum.DataAccess.Mappings
{
	using System.Data.Entity.ModelConfiguration;

	public class CardMap : EntityTypeConfiguration<Card>
	{
		public CardMap()
		{
			ToTable("Cards");
			HasKey(i => i.Id);
			Property(i => i.Name).IsRequired().HasMaxLength(50).IsRequired();
			Property(i => i.Description).IsRequired().HasMaxLength(250).IsRequired();
			Property(i => i.Link).IsRequired().HasMaxLength(1000).IsRequired();
			HasMany(i => i.Categories).WithMany(c => c.Cards)
				.Map(cs =>
				{
					cs.MapLeftKey("CardId");
					cs.MapRightKey("CategoryId");
					cs.ToTable("Categories");
				});
			;
		}
	}
}
using System.Data.Entity.ModelConfiguration;
using Forum.Domain;

namespace Forum.DataAccess.Mappings
{
	public class CategoryMap : EntityTypeConfiguration<Category>
	{
		public CategoryMap()
		{
			ToTable("Categories");
			HasKey(i => i.Id);
			Property(i => i.Name).IsRequired().HasMaxLength(50).IsRequired();
		}
	}
}
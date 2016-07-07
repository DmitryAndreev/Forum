namespace Forum.DataAccess.Mappings
{
	using System.Data.Entity.ModelConfiguration;
	using Domain;

	public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
		{
			HasKey(i => i.Id);
			Property(i => i.Header).IsRequired().HasMaxLength(250).IsRequired();
			Property(i => i.Body)
				.IsRequired()
				.HasMaxLength(null);
		}
	}
}
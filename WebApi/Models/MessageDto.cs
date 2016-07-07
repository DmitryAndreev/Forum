namespace Forum.WebApi.Models
{
	using System;
	using JetBrains.Annotations;

	[UsedImplicitly]
	public class MessageDto
	{
		[UsedImplicitly]
		public Guid Id { get; set; }

		[UsedImplicitly]
		public string Header { get; set; }

		[UsedImplicitly]
		public string Body { get; set; }
	}
}
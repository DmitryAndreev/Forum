namespace Forum.WebApi.Models
{
	using System;
	using FluentValidation.Attributes;
	using JetBrains.Annotations;
	using Validator;

	[Validator(typeof(MessageValidator))]
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
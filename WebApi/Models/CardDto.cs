using System.Collections.Generic;
using Forum.Domain;

namespace Forum.WebApi.Models
{
	using System;
	using FluentValidation.Attributes;
	using Validator;

	[Validator(typeof (CardValidator))]
	public class CardDto
	{
		public Guid Id { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public string Link { get; set; }
		public List<Category> Categories { get; set; }
	}
}
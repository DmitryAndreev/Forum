using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain
{
	public class Category
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }
		public ICollection<Card> Cards { get; set; }
	}
}
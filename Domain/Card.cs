using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain
{
	public class Card
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Description { get; set; }
		public string Name { get; set; }
		public string Link { get; set; }
		public ICollection<Category> Categories { get; set; }
	}
}
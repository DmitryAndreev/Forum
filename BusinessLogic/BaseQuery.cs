namespace Forum.BusinessLogic
{
	using System.Collections.Generic;

	public class BaseQuery
	{
		public BaseQuery()
		{
			Sortings = new List<Sorting>();
			Paging = new Paging();
		}

		public Paging Paging { get; set; }

		public List<Sorting> Sortings { get; set; }
	}
}
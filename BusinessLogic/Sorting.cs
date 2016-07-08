using System;

namespace Forum.BusinessLogic
{
	public class Sorting
	{
		public Sorting(string field, bool isAsc)
		{
			Field = field;
			this.IsAsc = isAsc;
		}

		public string Field { get; private set; }

		public bool IsAsc { get; private set; }

		public override string ToString()
		{
			return String.Format("{0} {1}", Field, IsAsc ? "ASC" : "DESC");
		}
	}
}
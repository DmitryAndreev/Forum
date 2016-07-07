namespace Forum.BusinessLogic
{
	public class Sorting
	{
		public Sorting(string field, bool isAsc)
		{
			Field = field;
			this.IsAsc = isAsc;
		}

		public string Field { get; }

		public bool IsAsc { get; }

		public override string ToString()
		{
			return $"{Field} {(IsAsc ? "ASC" : "DESC")}";
		}
	}
}
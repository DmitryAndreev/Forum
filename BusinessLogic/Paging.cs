namespace Forum.BusinessLogic
{
	public class Paging
	{
		public Paging()
		{
			Page = 1;
		}

		public int Page { get; set; }

		public int? PageSize { get; set; }

		public int PageSizeValue
		{
			get { return PageSize ?? 10; }
		}

		public int SkipCount
		{
			get { return (Page - 1)*PageSizeValue; }
		}

		public void FixPageForCount(int count)
		{
			if (count <= SkipCount)
			{
				Page = 1;
			}
		}
	}
}
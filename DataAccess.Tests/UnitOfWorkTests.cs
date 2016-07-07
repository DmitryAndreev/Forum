namespace DataAccess.Tests
{
	using System.Data.Entity;
	using System.Threading.Tasks;
	using Forum.DataAccess;
	using NUnit.Framework;
	using Moq;

	[TestFixture]
	public class UnitOfWorkTests
	{
		[Test]
		public async Task CheckCommit()
		{
			Mock<DbContext> mock = new Mock<DbContext>();
			UnitOfWork unitOfWork = new UnitOfWork(mock.Object);
			await unitOfWork.Commit();
			mock.Verify(work => work.SaveChangesAsync(), Times.Once);
		}
	}
}
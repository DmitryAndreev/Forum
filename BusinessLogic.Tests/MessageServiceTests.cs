namespace Forum.BusinessLogic.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using DataAccess;
	using Domain;
	using Moq;
	using NUnit.Framework;

	[TestFixture]
	public class MessageServiceTests
	{
		[Test]
		public async Task CheckCreateMessage()
		{
			var message = new Message { Body = "Test body", Header = "Test Header" };
			Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
			mock.SetupAllProperties();
			mock.Setup(work => work.MessageRepository.Add(message));
			var messageService = new MessageService(mock.Object, new FakeEntityExtensionMethodsWrapper());

			var newMessage = await messageService.Create(message);

			Assert.That(newMessage.Header, Is.EqualTo(message.Header));
			Assert.That(newMessage.Body, Is.EqualTo(message.Body));
			mock.Verify(work => work.Commit(), Times.Once);
			mock.Verify(work => work.MessageRepository.Add(message), Times.Once);
			mock.VerifyGet(work => work.MessageRepository, Times.Once);
		}

		[Test]
		public async Task CheckUpdateMessage()
		{
			var message = new Message { Body = "Test body", Header = "Test Header" };
			Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
			mock.SetupAllProperties();
			mock.Setup(work => work.MessageRepository.Update(message));
			var messageService = new MessageService(mock.Object, new FakeEntityExtensionMethodsWrapper());

			var newMessage = await messageService.Update(message);

			Assert.That(newMessage.Header, Is.EqualTo(message.Header));
			Assert.That(newMessage.Body, Is.EqualTo(message.Body));
			mock.Verify(work => work.Commit(), Times.Once);
			mock.Verify(work => work.MessageRepository.Update(message), Times.Once);
			mock.VerifyGet(work => work.MessageRepository, Times.Once);
		}

		[Test]
		public async Task CheckFindByIdMessage()
		{
			Guid messageId = Guid.NewGuid();
			var message = new Message { Body = "Test body", Header = "Test Header", Id = messageId};
			Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
			mock.SetupAllProperties();
			mock.Setup(work => work.MessageRepository.FindById(messageId)).Returns(Task.FromResult(message));
			var messageService = new MessageService(mock.Object, new FakeEntityExtensionMethodsWrapper());

			var newMessage = await messageService.Get(messageId);

			Assert.That(newMessage.Header, Is.EqualTo(message.Header));
			Assert.That(newMessage.Body, Is.EqualTo(message.Body));
			Assert.That(newMessage.Id, Is.EqualTo(message.Id));
			mock.Verify(work => work.MessageRepository.FindById(messageId), Times.Once);
			mock.VerifyGet(work => work.MessageRepository, Times.Once);
		}

		[Test]
		public async Task CheckDeleteMessage()
		{
			Guid messageId = Guid.NewGuid();
			var message = new Message { Body = "Test body", Header = "Test Header", Id = messageId };
			Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
			mock.SetupAllProperties();
			mock.Setup(work => work.MessageRepository.FindById(messageId)).Returns(Task.FromResult(message));
			var messageService = new MessageService(mock.Object, new FakeEntityExtensionMethodsWrapper());

			await messageService.Delete(messageId);
			
			mock.Verify(work => work.MessageRepository.Remove(message), Times.Once);
			mock.Verify(work => work.MessageRepository.FindById(messageId), Times.Once);
			mock.Verify(work => work.Commit(), Times.Once);
			mock.VerifyGet(work => work.MessageRepository, Times.Exactly(2));
		}

		[Test]
		public async Task CheckGetMessages()
		{
			List<Message> returnMessages = new List<Message>();
			for (int i = 1; i < 100; i++)
			{
				returnMessages.Add(new Message { Body = "Test body" + i, Header = "Test Header" + i, Id = Guid.NewGuid()});
			}


			Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
			//unitOfWork.SetupAllProperties();
			Mock<IQueryable<Message>> messages = new Mock<IQueryable<Message>>();
			unitOfWork.Setup(work => work.MessageRepository.All()).Returns(returnMessages.AsQueryable());
			var messageService = new MessageService(unitOfWork.Object, new FakeEntityExtensionMethodsWrapper());

			var paging = new Paging {Page = 1, PageSize = 10};
			var query = new BaseQuery { Paging = paging, Sortings = new List<Sorting> {new Sorting("Header", false)} };
			var queryResponse = await messageService.Get(query);

			var resultList = queryResponse.List;
			Assert.That(resultList, Is.Ordered.Descending.By("Header"));
			Assert.That(queryResponse.Page, Is.EqualTo(1));
			Assert.That(queryResponse.PageSize, Is.EqualTo(10));
			Assert.That(queryResponse.Total, Is.EqualTo(99));
			unitOfWork.Verify(work => work.MessageRepository.All(), Times.Once);

		}
	}

	public class FakeEntityExtensionMethodsWrapper : IExtensionMethodsWrapper
	{
		public Task<int> CountAsync<T>(IQueryable<T> source)
		{
			return Task.FromResult(source.Count());
		}

		public Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> source)
		{
			return Task.FromResult(source.ToList());
		}
	}
}
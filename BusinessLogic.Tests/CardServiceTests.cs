using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.DataAccess;
using Forum.Domain;
using Moq;
using NUnit.Framework;

namespace Forum.BusinessLogic.Tests
{
	[TestFixture]
	public class CardServiceTests
	{
		[Test]
		public async Task CheckCreateCard()
		{
			var card = new Card {Description = "Test description", Name = "Test name"};
			Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
			mock.SetupAllProperties();
			mock.Setup(work => work.CardRepository.Add(card));
			var cardService = new CardService(mock.Object, new FakeEntityExtensionMethodsWrapper());

			var newCard = await cardService.Create(card);

			Assert.That(newCard.Name, Is.EqualTo(card.Name));
			Assert.That(newCard.Description, Is.EqualTo(card.Description));
			mock.Verify(work => work.Commit(), Times.Once);
			mock.Verify(work => work.CardRepository.Add(card), Times.Once);
			mock.VerifyGet(work => work.CardRepository, Times.Once);
		}

		[Test]
		public async Task CheckUpdateCard()
		{
			var card = new Card {Name = "Test Name", Description = "Test description"};
			Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
			mock.SetupAllProperties();
			mock.Setup(work => work.CardRepository.Update(card));
			var cardService = new CardService(mock.Object, new FakeEntityExtensionMethodsWrapper());

			var newCard = await cardService.Update(card);

			Assert.That(newCard.Name, Is.EqualTo(card.Name));
			Assert.That(newCard.Description, Is.EqualTo(card.Description));
			mock.Verify(work => work.Commit(), Times.Once);
			mock.Verify(work => work.CardRepository.Update(card), Times.Once);
			mock.VerifyGet(work => work.CardRepository, Times.Once);
		}

		[Test]
		public async Task CheckFindByCardId()
		{
			Guid cardId = Guid.NewGuid();
			var card = new Card {Name = "Test name", Description = "Test description", Id = cardId};
			Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
			mock.SetupAllProperties();
			mock.Setup(work => work.CardRepository.FindById(cardId)).Returns(Task.FromResult(card));
			var cardService = new CardService(mock.Object, new FakeEntityExtensionMethodsWrapper());

			var newCard = await cardService.Get(cardId);

			Assert.That(newCard.Name, Is.EqualTo(card.Name));
			Assert.That(newCard.Description, Is.EqualTo(card.Description));
			Assert.That(newCard.Id, Is.EqualTo(card.Id));
			mock.Verify(work => work.CardRepository.FindById(cardId), Times.Once);
			mock.VerifyGet(work => work.CardRepository, Times.Once);
		}

		[Test]
		public async Task CheckDeleteCard()
		{
			Guid cardId = Guid.NewGuid();
			var card = new Card {Description = "Test body", Name = "Test description", Id = cardId};
			Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
			mock.SetupAllProperties();
			mock.Setup(work => work.CardRepository.FindById(cardId)).Returns(Task.FromResult(card));
			var cardService = new CardService(mock.Object, new FakeEntityExtensionMethodsWrapper());

			await cardService.Delete(cardId);

			mock.Verify(work => work.CardRepository.Remove(card), Times.Once);
			mock.Verify(work => work.CardRepository.FindById(cardId), Times.Once);
			mock.Verify(work => work.Commit(), Times.Once);
			mock.VerifyGet(work => work.CardRepository, Times.Exactly(2));
		}

		[Test]
		public async Task CheckGetCard()
		{
			List<Card> cards = new List<Card>();
			for (int i = 1; i < 100; i++)
			{
				cards.Add(new Card {Name = "Test name" + i, Description = "Test description" + i, Id = Guid.NewGuid()});
			}


			Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
			unitOfWork.Setup(work => work.CardRepository.All()).Returns(cards.AsQueryable());
			var cardService = new CardService(unitOfWork.Object, new FakeEntityExtensionMethodsWrapper());

			var paging = new Paging {Page = 1, PageSize = 10};
			var query = new BaseQuery {Paging = paging, Sortings = new List<Sorting> {new Sorting("Name", false)}};
			var queryResponse = await cardService.Get(query);

			var resultList = queryResponse.List;
			Assert.That(resultList, Is.Ordered.Descending.By("Name"));
			Assert.That(queryResponse.Page, Is.EqualTo(1));
			Assert.That(queryResponse.PageSize, Is.EqualTo(10));
			Assert.That(queryResponse.Total, Is.EqualTo(99));
			unitOfWork.Verify(work => work.CardRepository.All(), Times.Once);
		}
	}
}
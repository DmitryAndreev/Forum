using Forum.Domain;

namespace Forum.WebApi.Profiles
{
	using AutoMapper;
	using JetBrains.Annotations;
	using Models;

	[UsedImplicitly]
	public class CardProfile : Profile
	{
		public CardProfile()
		{
			CreateMap<Card, CardDto>();
			CreateMap<CardDto, Card>();
		}
	}
}
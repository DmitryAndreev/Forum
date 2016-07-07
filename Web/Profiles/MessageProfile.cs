namespace Forum.Web.Profiles
{
	using AutoMapper;
	using Domain;
	using JetBrains.Annotations;
	using Models;

	[UsedImplicitly]
	public class MessageProfile : Profile
	{
		public MessageProfile()
		{
			CreateMap<Message, MessageDto>();
			CreateMap<MessageDto, Message>();
		}
	}
}
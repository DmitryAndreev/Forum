namespace Forum.WebApi.Models.Validator
{
	using FluentValidation;
	public class MessageValidator : AbstractValidator<MessageDto>
	{
		public MessageValidator()
		{
			RuleFor(x => x.Id).NotNull();
			RuleFor(x => x.Header).NotNull().Length(1, 250);
			RuleFor(x => x.Body).NotNull();
		}
	}
}
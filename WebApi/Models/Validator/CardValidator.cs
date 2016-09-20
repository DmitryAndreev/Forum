namespace Forum.WebApi.Models.Validator
{
	using FluentValidation;

	public class CardValidator : AbstractValidator<CardDto>
	{
		public CardValidator()
		{
			RuleFor(x => x.Id).NotNull();
			RuleFor(x => x.Name).NotNull().Length(1, 50);
			RuleFor(x => x.Description).NotNull().Length(1, 250);
			RuleFor(x => x.Link).NotNull().Length(1, 1000);
		}
	}
}
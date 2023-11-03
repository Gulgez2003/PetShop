namespace Business.Utilities.Validators
{
    public class AnimalPostDTOValidator : AbstractValidator<AnimalPostDTO>
    {
        public AnimalPostDTOValidator()
        {
            RuleFor(a => a.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}

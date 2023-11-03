namespace Business.Utilities.Validators
{
    public class CategoryPostDTOValidator : AbstractValidator<CategoryPostDTO>
    {
        public CategoryPostDTOValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}

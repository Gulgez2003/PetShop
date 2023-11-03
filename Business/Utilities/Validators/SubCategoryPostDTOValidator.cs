namespace Business.Utilities.Validators
{
    public class SubCategoryPostDTOValidator : AbstractValidator<SubCategoryPostDTO>
    {
        public SubCategoryPostDTOValidator()
        {
            RuleFor(a => a.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}

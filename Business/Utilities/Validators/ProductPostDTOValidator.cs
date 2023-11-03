namespace Business.Utilities.Validators
{
    public class ProductPostDTOValidator : AbstractValidator<ProductPostDTO>
    {
        public ProductPostDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(p => p.Title)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .LessThan(300);
        }
    }
}

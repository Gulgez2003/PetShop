namespace Business.Utilities.Validators
{
    public class TestimonialPostDTOValidator : AbstractValidator<TestimonialPostDTO>
    {
        public TestimonialPostDTOValidator()
        {
            RuleFor(t => t.Comment)
                .NotNull()
                .NotEmpty()
                .MaximumLength(256)
                .MinimumLength(2);
            RuleFor(t => t.FullName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(t => t.Email)
               .NotNull()
               .NotEmpty()
               .MaximumLength(128)
               .EmailAddress()
               .MinimumLength(5);
        }
    }
}

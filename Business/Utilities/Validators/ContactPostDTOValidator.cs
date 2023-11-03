namespace Business.Utilities.Validators
{
    public class ContactPostDTOValidator : AbstractValidator<ContactPostDTO>
    {
        public ContactPostDTOValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(c => c.Message)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);
            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128)
                .EmailAddress()
                .MinimumLength(5);
        }
    }
}

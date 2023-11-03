namespace Business.Utilities.Validators
{
    public class OrderPostDTOValidator : AbstractValidator<OrderPostDTO>
    {
        public OrderPostDTOValidator()
        {
            RuleFor(o => o.FirstName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(o => o.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);   
            RuleFor(o => o.Comment)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);
            RuleFor(o => o.Email)
               .NotNull()
               .NotEmpty()
               .MaximumLength(128)
               .EmailAddress()
               .MinimumLength(5);
            RuleFor(o => o.Phone)
                .NotNull()
                .NotEmpty()
                .MaximumLength(15)
                .EmailAddress()
                .MinimumLength(9);
        }
    }
}

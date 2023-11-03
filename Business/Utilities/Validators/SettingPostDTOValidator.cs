namespace Business.Utilities.Validators
{
    public class SettingPostDTOValidator : AbstractValidator<SettingPostDTO>
    {
        public SettingPostDTOValidator()
        {
            RuleFor(s => s.Information)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128)
                .MinimumLength(2);
            RuleFor(s => s.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128)
                .EmailAddress()
                .MinimumLength(5);
            RuleFor(s => s.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(9);
            RuleFor(s => s.Address)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128)
                .MinimumLength(2);
            RuleFor(s => s.FaceBookIcon)
                .NotNull()
                .NotEmpty();
            RuleFor(s => s.TwitterIcon)
                .NotNull()
                .NotEmpty();
            RuleFor(s => s.InstagramIcon)
                .NotNull()
                .NotEmpty();
        }
    }
}

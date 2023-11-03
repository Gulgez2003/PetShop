using Entities.DTOs.ServiceDTOs;

namespace Business.Utilities.Validators
{
    public class ServicePostDTOValidator : AbstractValidator<ServicePostDTO>
    {
        public ServicePostDTOValidator()
        {
            RuleFor(s => s.Name)
               .NotNull()
               .NotEmpty()
               .MinimumLength(2)
               .MaximumLength(50);
            RuleFor(s => s.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);
            RuleFor(s => s.Icon)
                .NotNull()
                .NotEmpty();
        }
    }
}

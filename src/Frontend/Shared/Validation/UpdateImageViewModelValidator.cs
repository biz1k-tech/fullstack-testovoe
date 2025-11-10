using FluentValidation;
using Shared.ViewModel;

namespace Shared.Validation
{
    public class UpdateImageViewModelValidator : AbstractValidator<UpdateImageViewModel>
    {
        public UpdateImageViewModelValidator()
        {
            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithErrorCode(ValidationErrorCode.Empty)
                .MaximumLength(50).WithErrorCode(ValidationErrorCode.TooLong);
        }
    }
}

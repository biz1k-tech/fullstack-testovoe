using FluentValidation;
using Shared.ViewModel;

namespace Shared.Validation;

public class UpdateImageViewModelValidator : AbstractValidator<UpdateImageViewModel>
{
    public UpdateImageViewModelValidator()
    {
        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithErrorCode(ValidationErrorCode.Empty).WithMessage("Описание не может быть пустым.")
            .NotEmpty().WithErrorCode(ValidationErrorCode.Empty).WithMessage("Описание не может быть пустым.")
            .MaximumLength(50).WithErrorCode(ValidationErrorCode.TooLong).WithMessage("Описание не может быть длиннее 50 символов.");
    }
}
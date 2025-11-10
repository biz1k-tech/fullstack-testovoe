using Api.Models.Request;
using FluentValidation;

namespace Api.Common.Validation;

public class UpdateImageRequestValidator : AbstractValidator<UpdateImageRequest>
{
    public UpdateImageRequestValidator()
    {
        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithErrorCode(ValidationErrorCode.Empty)
            .MaximumLength(50).WithErrorCode(ValidationErrorCode.TooLong);
    }
}
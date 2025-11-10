using Api.Models.Request;
using FluentValidation;

namespace Api.Common.Validation
{
    public class CreateImageRequestValidator: AbstractValidator<CreateImageRequest>
    {
        private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".webm" };
        private readonly string[] permittedContentTypes = { "image/jpeg", "image/png", "image/jpg" };

        public CreateImageRequestValidator()
        {
            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithErrorCode(ValidationErrorCode.Empty)
                .MaximumLength(50).WithErrorCode(ValidationErrorCode.TooLong);

            RuleFor(x => x.File)
                .NotNull().WithErrorCode(ValidationErrorCode.Empty);

            RuleFor(x => x.File.ContentType)
                .Must(ct => permittedContentTypes.Contains(ct)).WithErrorCode(ValidationErrorCode.InvalidImageType);

            RuleFor(f => f.File.FileName)
                .Must(fn =>
                {
                    var ext = Path.GetExtension(fn).ToLowerInvariant();
                    return permittedExtensions.Contains(ext);
                }).WithErrorCode(ValidationErrorCode.InvalidExtensionType);
        }
    }
}

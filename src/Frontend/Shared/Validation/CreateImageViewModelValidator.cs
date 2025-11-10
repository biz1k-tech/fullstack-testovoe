using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Const;
using Shared.ViewModel;

namespace Shared.Validation;

public class CreateImageViewModelValidator : AbstractValidator<CreateImageViewModel>
{
    private const int MaxFileSizeMB = ValidationConst.MaxFileSizeMb;
    private readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webm" };

    public CreateImageViewModelValidator()
    {
        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithErrorCode(ValidationErrorCode.Empty)
            .MaximumLength(50).WithErrorCode(ValidationErrorCode.TooLong);

        RuleFor(x => x.ImageBrowserFile)
            .NotNull().WithMessage("Необходимо выбрать файл изображения.")
            .Must(BeAValidFileSize).WithMessage($"Размер файла не должен превышать {MaxFileSizeMB} MB.")
            .Must(BeAValidExtension)
            .WithMessage($"Разрешены только файлы {string.Join(", ", AllowedExtensions).Replace(".", "")}.");
    }

    private bool BeAValidFileSize(IBrowserFile? file)
    {
        return file != null && file.Size <= MaxFileSizeMB * 1024 * 1024;
    }

    private bool BeAValidExtension(IBrowserFile? file)
    {
        if (file == null) return true;
        var extension = Path.GetExtension(file.Name).ToLowerInvariant();
        return AllowedExtensions.Contains(extension);
    }
}
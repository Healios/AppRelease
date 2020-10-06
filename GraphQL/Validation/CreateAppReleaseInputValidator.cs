using Core;
using Core.Models;
using FluentValidation;
using GraphQL.Models.Inputs;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace GraphQL.Validation
{
    public class CreateAppReleaseInputValidator : AbstractValidator<CreateAppReleaseInput>
    {
        public CreateAppReleaseInputValidator(IStringLocalizer<ErrorResources> localizer)
        {
            RuleFor(appRelease => appRelease.Title).MinimumLength(5).MaximumLength(450);
            RuleFor(appRelease => appRelease.Description).MinimumLength(10).MaximumLength(800);
            RuleFor(appRelease => appRelease.Version).MinimumLength(5).MaximumLength(450).Custom((value, context) =>
            {
                if (!Regex.IsMatch(value, @"^\d+.\d+.\d+"))
                    context.AddFailure(localizer[ErrorCodes.AppReleaseVersionIncorrectFormat, value]);
            });
            RuleFor(appRelease => appRelease.APKFile).NotNull();
        }
    }
}

using Core;
using Core.Models;
using Core.Repositories;
using FluentValidation;
using GraphQL.Models.Inputs;
using HotChocolate;
using HotChocolate.Subscriptions;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;
using ErrorCodes = Core.Models.ErrorCodes;

namespace GraphQL.Mutations
{
    public class Mutation
    {
        private readonly IValidator<CreateAppReleaseInput> createAppReleaseInputValidator;
        private readonly IValidator<UpdateAppReleaseInput> updateAppReleaseInputvalidator;
        private readonly IAppReleaseRepository appReleaseRepository;
        private readonly IStringLocalizer<ErrorResources> localizer;

        public Mutation(IValidator<CreateAppReleaseInput> createAppReleaseInputValidator, IValidator<UpdateAppReleaseInput> updateAppReleaseInputvalidator, IAppReleaseRepository appReleaseRepository, IStringLocalizer<ErrorResources> localizer)
        {
            this.createAppReleaseInputValidator = createAppReleaseInputValidator;
            this.updateAppReleaseInputvalidator = updateAppReleaseInputvalidator;
            this.appReleaseRepository = appReleaseRepository;
            this.localizer = localizer;
        }

        public async Task<IAppReleaseResult> CreateAppRelease(CreateAppReleaseInput appReleaseInput, [Service] ITopicEventSender eventSender)
        {
            // Validate input.
            var validationResult = await createAppReleaseInputValidator.ValidateAsync(appReleaseInput);
            if (!validationResult.IsValid) return new Error(validationResult.Errors.FirstOrDefault()?.ErrorMessage);

            var appRelease = new AppRelease()
            {
                Title = appReleaseInput.Title,
                Description = appReleaseInput.Description,
                Version = appReleaseInput.Version,
                APKFile = appReleaseInput.APKFile,
            };

            var result = await appReleaseRepository.CreateAppRelease(appRelease);
            if (result is Error) return result;

            await eventSender.SendAsync("OnNewAppRelease", (AppRelease)result);
            return result;
        }

        public async Task<IAppReleaseResult> UpdateAppRelease(string id, UpdateAppReleaseInput appReleaseInput)
        {
            // Validate input.
            var validationResult = await updateAppReleaseInputvalidator.ValidateAsync(appReleaseInput);
            if (!validationResult.IsValid) return new Error(validationResult.Errors.FirstOrDefault()?.ErrorMessage);

            var appRelease = await appReleaseRepository.GetAppRelease(id);
            if (appRelease == null) return new Error(localizer[ErrorCodes.AppReleaseWithIdNotFound]);

            appRelease.Title = appReleaseInput.Title;
            appRelease.Description = appReleaseInput.Description;
            appRelease.Version = appReleaseInput.Version;

            return await appReleaseRepository.UpdateAppRelease(appRelease);
        }

        public async Task<IAppReleaseResult> DeleteAppRelease(string id)
        {
            var appRelease = await appReleaseRepository.GetAppRelease(id);
            if (appRelease == null) return new Error(localizer[ErrorCodes.AppReleaseWithIdNotFound]);

            return await appReleaseRepository.DeleteAppRelease(appRelease);
        }
    }
}

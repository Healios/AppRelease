using Core;
using Core.Models;
using Core.Repositories;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Queries
{
    public class Query
    {
        private readonly IAppReleaseRepository appReleaseRepository;
        private readonly IStringLocalizer<ErrorResources> localizer;

        public Query(IAppReleaseRepository appReleaseRepository, IStringLocalizer<ErrorResources> localizer)
        {
            this.appReleaseRepository = appReleaseRepository;
            this.localizer = localizer;
        }

        public async Task<IAppReleaseResult> GetLatestAppRelease(bool includeAPKFile = true)
        {
            var result = await appReleaseRepository.GetLatestAppRelease(includeAPKFile);
            if (result == null) return new Error(localizer[ErrorCodes.AppReleasesNotFound]);

            return result;
        }

        public async Task<IAppReleaseResult> GetAppRelease(string id)
        {
            var result = await appReleaseRepository.GetAppRelease(id);
            if (result == null) return new Error(localizer[ErrorCodes.AppReleaseWithIdNotFound]);

            return result;
        }

        public async Task<IAppReleaseResult> GetAppReleases()
        {
            var result = await appReleaseRepository.GetAppReleases();
            if (result == null || result.Count() == 0) return new Error(localizer[ErrorCodes.AppReleasesNotFound]);

            return new AppReleaseCollection(result);
        }
    }
}

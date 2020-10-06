using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IAppReleaseRepository
    {
        /// <summary>
        /// Creates an app release.
        /// </summary>
        /// <param name="release"></param>
        /// <returns></returns>
        Task<IAppReleaseResult> CreateAppRelease(AppRelease release);

        /// <summary>
        /// Updates the specified app release.
        /// </summary>
        /// <param name="release"></param>
        /// <returns></returns>
        Task<IAppReleaseResult> UpdateAppRelease(AppRelease release);

        /// <summary>
        /// Deletes the specified app release.
        /// </summary>
        /// <param name="release"></param>
        /// <returns></returns>
        Task<IAppReleaseResult> DeleteAppRelease(AppRelease release);

        /// <summary>
        /// Retrieves the latest app release.
        /// </summary>
        /// <param name="includeAPKFile"></param>
        /// <returns></returns>
        Task<AppRelease> GetLatestAppRelease(bool includeAPKFile);

        /// <summary>
        /// Retrieves the app release with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppRelease> GetAppRelease(string id);

        /// <summary>
        /// Retrieves all app relases.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AppRelease>> GetAppReleases();
    }
}

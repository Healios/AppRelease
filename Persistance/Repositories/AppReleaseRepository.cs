using Core;
using Core.Models;
using Core.Repositories;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    internal class AppReleaseRepository : IAppReleaseRepository
    {
        private readonly DatabaseContext db;
        private readonly IStringLocalizer<ErrorResources> localizer;

        public AppReleaseRepository(DatabaseContext db, IStringLocalizer<ErrorResources> localizer)
        {
            this.db = db;
            this.localizer = localizer;
        }


        public async Task<IAppReleaseResult> CreateAppRelease(AppRelease release)
        {
            var result = await db.AppReleases.AddAsync(release);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (UniqueConstraintException)
            {
                return new Error(localizer[ErrorCodes.AppReleaseAlreadyExists, release.Version]);
            }

            return result.Entity;
        }

        public async Task<IAppReleaseResult> UpdateAppRelease(AppRelease release)
        {
            var result = db.Update(release);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (UniqueConstraintException)
            {
                return new Error(localizer[ErrorCodes.AppReleaseAlreadyExists, release.Version]);
            }

            return result.Entity;
        }

        public async Task<IAppReleaseResult> DeleteAppRelease(AppRelease release)
        {
            var result = db.Remove(release);
            await db.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<AppRelease> GetLatestAppRelease(bool includeAPKFile = true)
        {
            if (!includeAPKFile)
                return await db.AppReleases.OrderBy(release => release.Created).Select(release => new AppRelease(release)).LastOrDefaultAsync();

            return await db.AppReleases.OrderBy(release => release.Created).LastOrDefaultAsync();
        }

        public async Task<AppRelease> GetAppRelease(string id) => await db.AppReleases.FindAsync(id);

        public async Task<IEnumerable<AppRelease>> GetAppReleases() => await db.AppReleases.Select(release => new AppRelease(release)).ToListAsync();
    }
}

using System.Collections.Generic;

namespace Core.Models
{
    public class AppReleaseCollection : IAppReleaseResult
    {
        public AppReleaseCollection(IEnumerable<AppRelease> appReleases)
        {
            AppReleases = appReleases;
        }
        
        public IEnumerable<AppRelease> AppReleases { get; set; }
    }
}

using System;

namespace Core.Models
{
    public class AppRelease : IAppReleaseResult
    {
        public AppRelease()
        {
        }

        public AppRelease(AppRelease releaseWithoutAPK)
        {
            Id = releaseWithoutAPK.Id;
            Title = releaseWithoutAPK.Title;
            Description = releaseWithoutAPK.Description;
            Version = releaseWithoutAPK.Version;
            Created = releaseWithoutAPK.Created;
            LastModified = releaseWithoutAPK.LastModified;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public byte[] APKFile { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}

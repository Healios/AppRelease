using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    internal class AppReleaseConfiguration : IEntityTypeConfiguration<AppRelease>
    {
        public void Configure(EntityTypeBuilder<AppRelease> builder)
        {
            builder.HasKey(release => release.Id);
            builder.Property(release => release.Id).ValueGeneratedOnAdd();

            builder.HasIndex(release => release.Version).IsUnique();
            builder.Property(release => release.Title).IsRequired();
            builder.Property(release => release.Description).IsRequired();
            builder.Property(release => release.APKFile).IsRequired();
        }
    }
}

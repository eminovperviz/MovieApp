using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieApp.Persistence.Configurations;
public sealed class DataAccessGroupConfiguration : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(50);
        builder.Property(x => x.Synopsis).HasMaxLength(250);
    }
}
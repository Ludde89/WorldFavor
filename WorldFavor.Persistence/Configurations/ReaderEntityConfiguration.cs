using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldFavor.Contracts.Entities;

namespace WorldFavor.Persistence.Configurations
{
    public class ReaderEntityConfiguration : IEntityTypeConfiguration<ReaderEntity>
    {
        public void Configure(EntityTypeBuilder<ReaderEntity> builder)
        {
            builder.HasIndex(x => x.Name);
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Books)
                .WithOne(x => x.Reader);
        }
    }
}
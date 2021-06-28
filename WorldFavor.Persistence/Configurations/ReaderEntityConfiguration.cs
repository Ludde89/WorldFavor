using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldFavor.Contracts.Entities;

namespace WorldFavor.Persistence.Configurations
{
    public class ReaderEntityConfiguration : IEntityTypeConfiguration<ReaderEntity>
    {
        public void Configure(EntityTypeBuilder<ReaderEntity> builder)
        {
            builder.HasKey(x => x.Name);
        }
    }
}
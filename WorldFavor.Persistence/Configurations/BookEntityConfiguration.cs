using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldFavor.Contracts.Entities;

namespace WorldFavor.Persistence.Configurations
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder
                .HasOne<ReaderEntity>(x => x.Reader);

            builder.HasKey(x => x.ISBN);

        }
    }
}
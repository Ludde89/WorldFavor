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
                .HasOne<ReaderEntity>(x => x.Reader)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.ReaderId)
                .IsRequired(false);

            builder.HasKey(x => x.ISBN);

        }
    }
}
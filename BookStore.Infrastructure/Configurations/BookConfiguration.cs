using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.AuthorId)
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

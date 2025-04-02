using LogAndStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogAndStore.DAL.Configurations
{
    public class MyDataConfiguration : IEntityTypeConfiguration<MyData>
    {
        public void Configure(EntityTypeBuilder<MyData> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Code)
                .IsRequired();

            builder.Property(x => x.Value)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.ToTable("MyData");
        }
    }
}
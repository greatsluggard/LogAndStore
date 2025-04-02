using LogAndStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogAndStore.DAL.Configurations
{
    public class RequestLogConfiguration : IEntityTypeConfiguration<RequestLog>
    {
        public void Configure(EntityTypeBuilder<RequestLog> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Timestamp)
                .IsRequired();

            builder.Property(x => x.MethodName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.RequestData)
                .IsRequired();

            builder.Property(x => x.ResponseData)
                .IsRequired(false);

            builder.Property(x => x.IsSuccess)
                .IsRequired();

            builder.Property(x => x.ErrorMessage)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.ToTable("RequestLogs");
        }
    }
}
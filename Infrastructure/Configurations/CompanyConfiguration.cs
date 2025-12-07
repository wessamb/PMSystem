using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMSystem.Domain.Entities;

namespace PMSystem.Infrastructure.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("NameCompany").HasMaxLength(255).HasColumnType("NVARCHAR").IsRequired();
         
            builder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(255).HasColumnType("NVARCHAR").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime2");
            builder.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("datetime2");
            builder.ToTable("Company");
        }
    }
}

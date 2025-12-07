using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMSystem.Domain.Entities;

namespace PMSystem.Infrastructure.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title)
           .IsRequired()
           .HasMaxLength(100);

            builder.Property(e => e.Description)
                   .HasMaxLength(500);
            builder.HasOne(x=>x.Company).WithMany(c=>c.Projects).HasForeignKey(e => e.CompanyId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            builder.Property(x => x.EndDate).HasColumnName("EndDate").HasColumnType("datetime2");
            builder.HasMany(p => p.Items)
         .WithOne(t => t.Project)
         .HasForeignKey(t => t.ProjectId)
         .OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Project");

        }
    }
}

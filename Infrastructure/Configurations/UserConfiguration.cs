using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMSystem.Domain.Entities;

namespace PMSystem.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            
            builder.Property(u => u.Fullname)
                   .IsRequired()
                   .HasMaxLength(100);

           
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            
            builder.Property(u => u.RoleType)
                   .HasConversion<int>()
                   .IsRequired();

          
            builder.HasOne(u => u.Company)
                   .WithMany(c => c.Users)
                   .HasForeignKey(u => u.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict); 

          
            builder.HasMany(u => u.TaskItems)
                   .WithOne(t => t.AssignedUser)
                   .HasForeignKey(t => t.AssignedUserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("User");
        }
    }
}

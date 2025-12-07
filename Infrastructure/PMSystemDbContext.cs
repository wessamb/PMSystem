using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMSystem.Domain.Entities;

namespace PMSystem.Infrastructure
{
    public class PMSystemDbContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<User>Users { get; set; }
        public DbSet<Company> Company { get; set; }

        public DbSet<TaskItem> TaskItems { get; set; }

        public DbSet<Project> Projects { get; set; }


        public PMSystemDbContext(DbContextOptions<PMSystemDbContext> options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PMSystemDbContext).Assembly);
        }
    }
}

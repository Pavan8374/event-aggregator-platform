using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Basic properties
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            // Email value object
            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(256);

                email.HasIndex(e => e.Value)
                    .IsUnique();
            });

            // Password value object
            builder.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.Hash)
                    .HasColumnName("PasswordHash")
                    .IsRequired()
                    .HasMaxLength(256);

                password.Property(p => p.Salt)
                    .HasColumnName("PasswordSalt")
                    .IsRequired()
                    .HasMaxLength(256);
            });

            // Role relationship
            builder.Property(u => u.RoleId)
                .IsRequired();

            builder.HasOne<Role>()
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete

            // Additional indexes for better performance
            builder.HasIndex(u => u.LastLoginAt);
            builder.HasIndex(u => u.CreatedAt);
            builder.HasIndex(u => u.IsActive);

            // Default Attendee role
            // This uses the same deterministic GUID from the RoleConfiguration
            builder.Property(u => u.RoleId)
                .HasDefaultValue(Guid.Parse("e8311047-8829-4aa0-9d4a-39006e8e01c8"));  // Attendee role ID
        }
    }
}

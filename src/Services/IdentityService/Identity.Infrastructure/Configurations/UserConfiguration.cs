using Identity.Domain.Entities;
using Identity.Domain.Enumerations;
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

            // Additional indexes for better performance
            builder.HasIndex(u => u.LastLoginAt);
            builder.HasIndex(u => u.CreatedAt);
            builder.HasIndex(u => u.IsActive);

            // Default Attendee role
            // This uses the same deterministic GUID from the RoleConfiguration
            builder.Property(u => u.RoleId)
                .HasDefaultValue(RoleType.Attendee.Id);  // Attendee role ID
        }
    }
}

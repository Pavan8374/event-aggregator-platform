using Identity.Domain.Entities;
using Identity.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Configure entity properties
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Description)
                .HasMaxLength(500);

            // Configure the RoleType property
            builder.Property(r => r.Type)
                .HasConversion(
                    rt => rt.Id,  // To database
                    id => RoleType.FromValue<RoleType>(id) // From database
                )
                .IsRequired();

            builder.HasIndex(r => r.Name)
                .IsUnique();

            // Data seeding
            builder.HasData(
            CreateRole(Guid.Parse("e8311047-8829-4aa0-9d4a-39006e8e01c8"), RoleType.Attendee, "Regular users who attend events"),
            CreateRole(Guid.Parse("ee8611ad-bb96-4a42-97a1-2bd99763abe5"), RoleType.Organizer, "Users who can create and manage events"),
            CreateRole(Guid.Parse("c3a1a8c4-524f-4fd5-8ebd-7d87790347d3"), RoleType.Vendor, "Partner businesses that can offer services"),
            CreateRole(Guid.Parse("bd2f7c20-d56c-409a-a60b-7c178e0130c5"), RoleType.SpeakerPerformer, "Special guests who present or perform at events"),
            CreateRole(Guid.Parse("64f55c17-c5d1-4c1e-8cbb-1a48f6c3c1d5"), RoleType.Admin, "Platform administrators with full system access"),
            CreateRole(Guid.Parse("a8a05f76-9c4f-4dd9-a8fd-dd9d927aad95"), RoleType.SystemService, "Internal system services for automation"),
            CreateRole(Guid.Parse("3c94539e-8c14-4e98-9241-64ba31a6da54"), RoleType.Moderator, "Users who can moderate content and discussions"),
            CreateRole(Guid.Parse("b7f76f67-2b1b-4d4e-a13c-95d5022bc714"), RoleType.Advertiser, "Users who can create and manage advertisements")
            );
        }

        private static Role CreateRole(Guid id, RoleType type, string description)
        {
            var role = Role.CreateDefault(type);
            role.Update(type.Name, description);

            // Set the fixed ID
            typeof(Role).GetProperty("Id")?.SetValue(role, id);

            return role;
        }
    }
}

using Event.Domain.Common;
using Event.Domain.Enumerations;
using Event.Domain.ValueObjects.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Event.Infrastructure.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event.Domain.Entities.Event>
    {
        public void Configure(EntityTypeBuilder<Event.Domain.Entities.Event> builder)
        {
            builder.ToTable("Events");

            // Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Description)
                .HasColumnName("Description");

            builder.Property(e => e.Category)
                .HasConversion(new EnumerationConverter<EventCategory>())
                .HasColumnName("Category")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.OrganizerId)
                .HasColumnName("OrganizerId")
                .IsRequired();

            builder.Property(e => e.OrganizerName)
                .HasColumnName("OrganizerName")
                .HasMaxLength(255);

            builder.Property(e => e.Venue)
                .HasColumnName("Venue")
                .IsRequired()
                .HasMaxLength(255);

            // Complex properties
            builder.OwnsOne(e => e.TimeRange, range =>
            {
                range.Property(r => r.StartTime)
                    .HasColumnName("StartTime")
                    .IsRequired();

                range.Property(r => r.EndTime)
                    .HasColumnName("EndTime")
                    .IsRequired();
            });

            builder.OwnsOne(e => e.Capacity, capacity =>
            {
                capacity.Property(c => c.TotalSeats)
                    .HasColumnName("TotalSeats")
                    .IsRequired();

                capacity.Property(c => c.AvailableSeats)
                    .HasColumnName("AvailableSeats")
                    .IsRequired();
            });

            builder.OwnsOne(e => e.TicketPrice, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("Amount")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValue(0.00);

                price.Property(p => p.Currency)
                    .HasConversion(new EnumerationConverter<Currency>())
                    .HasColumnName("Currency")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'INR'");
            });

            builder.Property(e => e.IsFree)
                .HasColumnName("IsFree")
                .HasDefaultValue(false);

            builder.Property(e => e.ImageUrl)
                .HasColumnName("ImageUrl")
                .HasMaxLength(255);

            builder.Property(e => e.Status)
                .HasConversion(new EnumerationConverter<EventStatus>())
                .HasColumnName("Status")
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValueSql("'Pending'");

            builder.Property(e => e.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            // Ignore domain events
            builder.Ignore(e => e.DomainEvents);
        }
    }

    // Custom converter for Enumeration types
    public class EnumerationConverter<TEnum> : ValueConverter<TEnum, string>
        where TEnum : Enumeration
    {
        public EnumerationConverter()
            : base(
                v => v.Name,
                v => Enumeration.FromDisplayName<TEnum>(v))
        {
        }
    }
}

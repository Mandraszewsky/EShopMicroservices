using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(o => o.OrderName, complexBuilder =>
        {
            complexBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.ShippingAddress, complexBuilder =>
        {
            complexBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            complexBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

            complexBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

            complexBuilder.Property(a => a.AddressLine)
                .HasMaxLength(100)
                .IsRequired();

            complexBuilder.Property(a => a.Country)
                .HasMaxLength(50);

            complexBuilder.Property(a => a.State)
                .HasMaxLength(50);

            complexBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.BillingAddress, complexBuilder =>
        {
            complexBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            complexBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

            complexBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

            complexBuilder.Property(a => a.AddressLine)
                .HasMaxLength(100)
                .IsRequired();

            complexBuilder.Property(a => a.Country)
                .HasMaxLength(50);

            complexBuilder.Property(a => a.State)
                .HasMaxLength(50);

            complexBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.Payment, complexBuilder =>
        {
            complexBuilder.Property(p => p.CardName)
                .HasMaxLength(50);

            complexBuilder.Property(p => p.CardNumber)
                .HasMaxLength(24);

            complexBuilder.Property(p => p.Expiration)
                .HasMaxLength(10);

            complexBuilder.Property(p => p.CVV)
                .HasMaxLength(3);

            complexBuilder.Property(p => p.PaymentMethod)
                .HasMaxLength(50);
        });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(os => os.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(o => o.TotalPrice);
    }
}

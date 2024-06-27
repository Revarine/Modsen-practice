using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Models;

namespace Shop.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .HasOne(e => e.Order)
            .WithMany(e => e.OrderItems)
            .HasForeignKey(e => e.OrderId);

        builder
            .HasOne(e => e.Product)
            .WithMany(e => e.OrderItems)
            .HasForeignKey(e => e.ProductId);
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace order_processing.Models;

[PrimaryKey("ProductId", "OrderId")]
[Table("product_has_order")]
[Index("OrderId", Name = "fk_product_has_order_order1_idx")]
[Index("ProductId", Name = "fk_product_has_order_product_idx")]
public partial class ProductHasOrder
{
    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("ProductHasOrders")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductHasOrders")]
    public virtual Product Product { get; set; } = null!;
}
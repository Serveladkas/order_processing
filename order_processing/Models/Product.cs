using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace order_processing.Models;

[Table("product")]
public partial class Product
{
    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("title")]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Column("price")]
    [Precision(8, 2)]
    public decimal Price { get; set; }

    [Column("stock_quantity")]
    public int StockQuantity { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<ProductHasOrder> ProductHasOrders { get; set; } = new List<ProductHasOrder>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductLog> ProductLogs { get; set; } = new List<ProductLog>();
}
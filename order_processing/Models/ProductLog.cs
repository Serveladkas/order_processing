using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace order_processing.Models;

[Table("product_log")]
[Index("ProductId", Name = "product_id")]
public partial class ProductLog
{
    [Key]
    [Column("log_id")]
    public int LogId { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("action")]
    [StringLength(20)]
    public string Action { get; set; } = null!;

    [Column("action_time", TypeName = "datetime")]
    public DateTime ActionTime { get; set; }

    [Column("product_title")]
    [StringLength(100)]
    public string ProductTitle { get; set; } = null!;

    [Column("product_price")]
    [Precision(8, 2)]
    public decimal ProductPrice { get; set; }

    [Column("product_stock_quantity")]
    public int ProductStockQuantity { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductLogs")]
    public virtual Product Product { get; set; } = null!;
}
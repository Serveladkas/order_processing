using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace order_processing.Models;

[Table("order")]
[Index("SupplierId", Name = "fk_order_supplier1_idx")]
public partial class Order
{
    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("creation_date", TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    [Column("status")]
    [StringLength(45)]
    public string Status { get; set; } = null!;

    [Column("supplier_id")]
    public int SupplierId { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<ProductHasOrder> ProductHasOrders { get; set; } = new List<ProductHasOrder>();

    [ForeignKey("SupplierId")]
    [InverseProperty("Orders")]
    public virtual Supplier Supplier { get; set; } = null!;
}
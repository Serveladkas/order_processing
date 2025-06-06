using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace order_processing.Models;

[Table("supplier")]
public partial class Supplier
{
    [Key]
    [Column("supplier_id")]
    public int SupplierId { get; set; }

    [Column("title")]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Column("address")]
    [StringLength(150)]
    public string Address { get; set; } = null!;

    [Column("telephone")]
    [StringLength(25)]
    public string Telephone { get; set; } = null!;

    [InverseProperty("Supplier")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
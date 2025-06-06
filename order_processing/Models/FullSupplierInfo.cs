using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace order_processing.Models;

[Keyless]
public partial class FullSupplierInfo
{
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
}
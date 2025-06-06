using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace order_processing.Models;

[Keyless]
public partial class BasicSupplierInfo
{
    [Column("supplier_id")]
    public int SupplierId { get; set; }

    [Column("title")]
    [StringLength(100)]
    public string Title { get; set; } = null!;
} 
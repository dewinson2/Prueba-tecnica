using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prueba.Models;

public partial class ProductVariation
{
  [Key]
  public int VariationId { get; set; }

    public string Color { get; set; } = null!;

    public decimal Price { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}

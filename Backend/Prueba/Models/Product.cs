using System;
using System.Collections.Generic;

namespace Prueba.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<ProductVariation> ProductVariations { get; set; } = new List<ProductVariation>();
}

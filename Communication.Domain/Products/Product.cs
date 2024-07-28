using Core.Data;

namespace Communication.Domain.Products;

public class Product : IEntity
{
    public string Name { get; set; } = default!;
}
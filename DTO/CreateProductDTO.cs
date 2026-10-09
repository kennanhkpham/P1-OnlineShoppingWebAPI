namespace P1_OnlineShoppingWebAPI.DTOs;

public class CreateProductDTO
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool InStock { get; set; }
}
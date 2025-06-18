namespace ProductService.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // ← NEW (so your POST accepts it)
        public string ImageUrl { get; set; } = string.Empty;
    }


    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // ← NEW
        public string ImageUrl { get; set; } = string.Empty;
    }

}

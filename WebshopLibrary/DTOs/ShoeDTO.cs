namespace WebshopLibrary.DTOs
{
	public class ShoeDTO
	{
		public int Id { get; set; }
		public int ModelId { get; set; }
		public string? Brand { get; set; }
		public string? Name { get; set; }
		public string? Size { get; set; }
		public string? Color { get; set; }
		public string? ImgUrl { get; set; }
		public int Price { get; set; }
		public int Quantity { get; set; }
		public bool OnSale { get; set; }

	}
}

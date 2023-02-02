namespace sneakers_api_.net.models
{
	public class Sneaker
	{
		public int id { get; set; }
		public string brand { get; set; }
		public string model { get; set; }
		public string price { get; set; }
		public string[] sizes { get; set; }
		public string gender { get; set; }
		public string[] colors { get; set; }
		public string[] images_urls { get; set; }
	}
}

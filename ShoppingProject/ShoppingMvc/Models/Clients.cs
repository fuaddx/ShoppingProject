namespace ShoppingMvc.Models
{
	public class Clients:BaseEntity
	{
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}

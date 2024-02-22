namespace ShoppingMvc.ViewModels.ExpertsVm
{
	public class ExpertUpdateVm
	{
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
		public IFormFile MainImage { get; set; }
		public string Name { get; set; }
		public string Profession { get; set; }
	}
}

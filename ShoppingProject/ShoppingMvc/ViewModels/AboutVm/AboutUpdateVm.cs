namespace ShoppingMvc.ViewModels.AboutVm
{
    public class AboutUpdateVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile MainImage { get; set; }
    }
}

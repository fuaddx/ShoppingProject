namespace ShoppingMvc.ViewModels.AboutVm
{
    public class AboutCreateVm
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public IFormFile MainImage { get; set; }
    }
}

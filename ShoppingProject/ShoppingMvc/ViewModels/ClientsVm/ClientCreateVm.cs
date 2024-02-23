namespace ShoppingMvc.ViewModels.ClientsVm
{
    public class ClientCreateVm
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
        public IFormFile MainImage { get; set; }
    }
}

namespace ShoppingMvc.ViewModels.ClientsVm
{
    public class ClientUpdateVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile MainImage { get; set; }
    }
}

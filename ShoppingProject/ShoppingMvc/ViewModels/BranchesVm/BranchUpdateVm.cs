namespace ShoppingMvc.ViewModels.BranchesVm
{
    public class BranchUpdateVm
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile MainImage { get; set; }
    }
}

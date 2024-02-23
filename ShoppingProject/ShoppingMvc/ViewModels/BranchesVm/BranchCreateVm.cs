namespace ShoppingMvc.ViewModels.BranchesVm
{
    public class BranchCreateVm
    {
        public string Place { get; set; }
        public string Address { get; set; }
        public IFormFile MainImage { get; set; }
    }
}

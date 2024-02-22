using ShoppingMvc.ViewModels.HomeVms;
using ShoppingMvc.ViewModels.ProductDetailVms;

namespace ShoppingMvc.ViewModels.CombinedViewModel
{
    public class CombinedvmModel
    {
        public HomeVm HomeVm { get; set; }
        public ProductDetailVm ProductDetailVm { get; set; }
    }
}

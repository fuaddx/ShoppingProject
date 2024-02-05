using ShoppingMvc.ViewModels.CommonVm;
using ShoppingMvc.ViewModels.ProductVm;
using ShoppingMvc.ViewModels.SliderVm;

namespace ShoppingMvc.ViewModels.HomeVm
{
    public class HomeVm
    {
        public IEnumerable<SliderListItemVm> SliderListItems { get; set; }
       
    }
}

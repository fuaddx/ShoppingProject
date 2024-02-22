using ShoppingMvc.ViewModels.ProductDetailVms;
using ShoppingMvc.ViewModels.ProductVm;

namespace ShoppingMvc.ViewModels.ProductLitVm
{
    public class ProductListVm
    {
        public ProductDetailVm ProductDetail { get; set; }
        public List<ProductListItemVm> ProductListItems { get; set; }
    }
}

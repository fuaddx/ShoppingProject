using ShoppingMvc.Models;
using ShoppingMvc.Models.Identity;
using ShoppingMvc.ViewModels.CategoryVm;
using ShoppingMvc.ViewModels.CommentVm;
using ShoppingMvc.ViewModels.CommonVm;
using ShoppingMvc.ViewModels.ProductDetailVms;
using ShoppingMvc.ViewModels.ProductVm;
using ShoppingMvc.ViewModels.SellerVm;
using ShoppingMvc.ViewModels.SliderVm;

namespace ShoppingMvc.ViewModels.HomeVms
{
    public class HomeVm
    {
        public PaginationVm<IEnumerable<ProductListItemVm>> PaginationProduct { get; set; }
        public IEnumerable<SliderListItemVm> SliderListItems { get; set; }
        public ProductDetailVm ProductDetail { get; set; }
        public Product Products { get; set; }
        public IEnumerable<ProductListItemVm> ProductListItems { get; set; }
        public IEnumerable<CategoryListItemVm> CategoryListItemVms { get; set; }
        public List<Comment> Comments { get; set; }
        public IEnumerable<CommentViewModel> CommentViewModels { get; set; }

        
        public Comment Comment { get; set; }

        public HomeVm()
        {
            CategoryListItemVms = new List<CategoryListItemVm>();
            Comments = new List<Comment>();
            Comment = new Comment(); 
        }
    }
}

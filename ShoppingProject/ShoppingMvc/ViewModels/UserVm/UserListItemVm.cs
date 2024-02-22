using ShoppingMvc.Models.Identity;
using ShoppingMvc.Models;
using ShoppingMvc.ViewModels.CommonVm;
using ShoppingMvc.ViewModels.ProductVm;

namespace ShoppingMvc.ViewModels.UserVm
{
    public class UserListItemVm
    {
        public int? Id { get; set; }
        public SellerData SellerData { get; set; }
        public AppUser User { get; set; }

        public PaginationVm<IEnumerable<ProductListItemVm>>? Products { get; set; }
    }
}

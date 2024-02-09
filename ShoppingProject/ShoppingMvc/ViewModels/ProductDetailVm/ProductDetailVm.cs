using ShoppingMvc.Models;

namespace ShoppingMvc.ViewModels.ProductDetailVm
{
    public class ProductDetailVm
    {
        public List<Comment> Comments { get; set; }

        public ProductDetailVm()
        {
            Comments = new List<Comment>();
        }
    }
}

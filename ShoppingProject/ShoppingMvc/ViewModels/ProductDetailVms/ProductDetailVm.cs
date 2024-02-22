using ShoppingMvc.Models;
using ShoppingMvc.Models.Identity;
using ShoppingMvc.ViewModels.CommentVm;
using ShoppingMvc.ViewModels.HomeVms;
using ShoppingMvc.ViewModels.ProductVm;

namespace ShoppingMvc.ViewModels.ProductDetailVms
{
    public class ProductDetailVm
    {
        public List<Comment> Comments { get; set; }
        public IEnumerable<ProductListItemVm> ProductListItems { get; set; }
        public ProductListItemVm Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public CommentViewModel Comment { get; set; }
        public bool IsAuthenticatedUserExists { get; set; }
        public int TotalCommentsCount { get; set; }
        public double TotalRatingPercentage{ get; set; }

        public ProductDetailVm()
        {
            Comments = new List<Comment>();
            ProductListItems = new List<ProductListItemVm>();  
            Product = new ProductListItemVm();
            Categories = new List<Category>();
            Comment = new CommentViewModel();
            TotalCommentsCount = 0;
            TotalRatingPercentage = 0;
        }
    }

}

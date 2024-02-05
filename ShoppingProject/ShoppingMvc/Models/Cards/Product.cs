
using ShoppingMvc.Models.Categories;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingMvc.Models.Cards
{
    public class Product:BaseEntity
    {
        public string? Description { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal SellPrice { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal CostPrice { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? ImageUrl { get; set; }
        public int RateRange { get; set; }
        public string Title { get; set; }
    }
}

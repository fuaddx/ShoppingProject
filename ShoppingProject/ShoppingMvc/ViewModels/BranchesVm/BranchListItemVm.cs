namespace ShoppingMvc.ViewModels.BranchesVm
{
    public class BranchListItemVm
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool? isBanned { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsArchived { get; set; }
        public string ImageUrl { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
    }
}

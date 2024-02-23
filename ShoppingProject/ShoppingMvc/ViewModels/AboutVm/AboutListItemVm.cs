namespace ShoppingMvc.ViewModels.AboutVm
{
    public class AboutListItemVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        public bool? isBanned { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsArchived { get; set; }
        
    }
}

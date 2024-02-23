namespace ShoppingMvc.ViewModels.ClientsVm
{
    public class ClientListItemVm
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool? isBanned { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsArchived { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
    }
}

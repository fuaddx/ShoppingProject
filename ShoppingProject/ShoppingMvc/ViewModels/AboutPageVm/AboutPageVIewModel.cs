using ShoppingMvc.ViewModels.AboutVm;
using ShoppingMvc.ViewModels.BranchesVm;
using ShoppingMvc.ViewModels.ClientsVm;
using ShoppingMvc.ViewModels.ExpertsVm;

namespace ShoppingMvc.ViewModels.AboutPageVm
{
    public class AboutPageVIewModel
    {
        public IEnumerable<AboutListItemVm> AboutListItemvms { get; set; }
        public IEnumerable<ClientListItemVm> ClientListItems { get; set; }
        public IEnumerable<ExpertListItemVm> ExpertListItems { get; set; }
        public IEnumerable<BranchListItemVm> BranchListItems { get; set; }
    }
}

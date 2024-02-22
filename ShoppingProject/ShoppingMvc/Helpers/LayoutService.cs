using ShoppingMvc.Contexts;
using ShoppingMvc.Models;

namespace ShoppingMvc.Helpers
{
    public class LayoutService
    {
        EvaraDbContext _db {  get; set; }

        public LayoutService(EvaraDbContext db)
        {
            _db = db;
        }

        public async Task<Setting> GetSeedData()
            => await _db.Settings.FindAsync(1);
    }
}

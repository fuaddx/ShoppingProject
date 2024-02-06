using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingMvc.Models;
using ShoppingMvc.Models.Cards;
using ShoppingMvc.Models.Categories;

namespace ShoppingMvc.Contexts
{
    public class EvaraDbContext : DbContext
    {
        public EvaraDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category>  Categorys { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken=default) 
        {
            IEnumerable<EntityEntry<Slider>> entries = ChangeTracker.Entries<Slider>();
            IEnumerable<EntityEntry<Category>> entries1 = ChangeTracker.Entries<Category>();
            IEnumerable<EntityEntry<Product>> entries2 = ChangeTracker.Entries<Product>();
            TimeZoneInfo aztTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");

            foreach (var entry in entries)
            {
                DateTime currentTimeUtc = DateTime.UtcNow;
                DateTime currentTimeAzt = TimeZoneInfo.ConvertTimeFromUtc(currentTimeUtc, aztTimeZone);

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedTime = currentTimeAzt;
                    entry.Entity.UpdatedTime = null;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedTime = currentTimeAzt;
                    var modifiedProps = entry.Properties.Where(prop => prop.IsModified && !prop.Metadata.IsPrimaryKey());
                    if (!modifiedProps.Any())
                    {
                        entry.Entity.UpdatedTime = null;
                    }
                }
            }
            foreach (var entry in entries1)
            {
                DateTime currentTimeUtc = DateTime.UtcNow;
                DateTime currentTimeAzt = TimeZoneInfo.ConvertTimeFromUtc(currentTimeUtc, aztTimeZone);

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedTime = currentTimeAzt;
                    entry.Entity.UpdatedTime = null;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedTime = currentTimeAzt;
                    var modifiedProps = entry.Properties.Where(prop => prop.IsModified && !prop.Metadata.IsPrimaryKey());
                    if (!modifiedProps.Any())
                    {
                        entry.Entity.UpdatedTime = null;
                    }
                }
            }
            foreach (var entry in entries2)
            {
                DateTime currentTimeUtc = DateTime.UtcNow;
                DateTime currentTimeAzt = TimeZoneInfo.ConvertTimeFromUtc(currentTimeUtc, aztTimeZone);

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedTime = currentTimeAzt;
                    entry.Entity.UpdatedTime = null;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedTime = currentTimeAzt;
                    var modifiedProps = entry.Properties.Where(prop => prop.IsModified && !prop.Metadata.IsPrimaryKey());
                    if (!modifiedProps.Any())
                    {
                        entry.Entity.UpdatedTime = null;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

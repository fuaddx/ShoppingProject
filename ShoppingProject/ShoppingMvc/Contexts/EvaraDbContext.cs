using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingMvc.Models;
using ShoppingMvc.Models.Cards;
using ShoppingMvc.Models.Categories;
using ShoppingMvc.Models.Tags;

namespace ShoppingMvc.Contexts
{
    public class EvaraDbContext : IdentityDbContext
    {
        public EvaraDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category>  Categorys { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replys { get; set; }
        public DbSet<Setting> Settings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reply>()
                .HasOne(r => r.Comment)
                .WithMany(c => c.Replies)
                .HasForeignKey(r => r.CommentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Setting>().HasData(new Setting
            {
                Id = 1,
                Logo = "http://localhost:5063/Assets/assets/imgs/theme/logo.svg",
                Address = "562 Wellington Road, Street 32, San Francisco",
                PhoneNumber = "+01 2222 365 /(+91) 01 2345 6789",
                Hours = "10:00 - 18:00, Mon - Sat"
            });
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken=default) 
        {
            IEnumerable<EntityEntry<Slider>> entries = ChangeTracker.Entries<Slider>();
            IEnumerable<EntityEntry<Category>> entries1 = ChangeTracker.Entries<Category>();
            IEnumerable<EntityEntry<Product>> entries2 = ChangeTracker.Entries<Product>();
			IEnumerable<EntityEntry<Tag>> entries3 = ChangeTracker.Entries<Tag>();
            IEnumerable<EntityEntry<Setting>> entries4 = ChangeTracker.Entries<Setting>();
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
            foreach (var entry in entries3)
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
            foreach (var entry in entries4)
            {
                DateTime currentTimeUtc = DateTime.UtcNow;
                DateTime currentTimeAzt = TimeZoneInfo.ConvertTimeFromUtc(currentTimeUtc, aztTimeZone);

                if (entry.State == EntityState.Modified)
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

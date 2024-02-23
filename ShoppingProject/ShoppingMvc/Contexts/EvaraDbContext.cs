using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingMvc.Models;
using ShoppingMvc.Models.Identity;
using System.Reflection.Metadata;

namespace ShoppingMvc.Contexts
{
    public class EvaraDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public EvaraDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replys { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<AdditionalInfo> AdditionalInfos { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<SellerData> SellerDatas { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Experts> Expertss { get; set; }
        public DbSet<Clients> Client { get; set; }
        public DbSet<Branches> Brachess { get; set; }
        public DbSet<About> Aboutt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reply>()
                .HasOne(r => r.Comment)
                .WithMany(c => c.Replies)
                .HasForeignKey(r => r.CommentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.SellerData)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TimeZoneInfo aztTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            IEnumerable<EntityEntry<AppUser>> entries = ChangeTracker.Entries<AppUser>();
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
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
            foreach (var entry in ChangeTracker.Entries<AppUser>())
            {
                if (entry.State == EntityState.Added)
                {
                    DateTime currentTimeUtc = DateTime.UtcNow;
                    DateTime currentTimeAzt = TimeZoneInfo.ConvertTimeFromUtc(currentTimeUtc, aztTimeZone);

                    // Log information
                    Console.WriteLine($"Adding new AppUser. Current UTC time: {currentTimeUtc}, Current AZT time: {currentTimeAzt}");

                    entry.Entity.CreatedTime = currentTimeAzt;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }



    }
}

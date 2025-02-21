using Backend.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Backend.Api.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameKey> GameKeys { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<GameKey>()
                .HasOne(x => x.Game)
                .WithMany(x => x.GameKeys)
                .HasForeignKey(x => x.GameId);

            modelBuilder.Entity<Game>()
                .HasMany(x => x.GameKeys)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId);

            modelBuilder.Entity<Game>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.GenreId);

            modelBuilder.Entity<Game>()
                .HasOne(x => x.Features)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.FeaturesId);

            modelBuilder.Entity<Genre>()
                .HasMany(x => x.Games)
                .WithOne(x => x.Genre)
                .HasForeignKey(x => x.GenreId);

            modelBuilder.Entity<Feature>()
                .HasMany(x => x.Games)
                .WithOne(x => x.Features)
                .HasForeignKey(x => x.FeaturesId);


            modelBuilder.Entity<BasketItem>()
                .HasOne(bi => bi.Basket)
                .WithMany(b => b.Items)
                .HasForeignKey(bi => bi.BasketId);

            modelBuilder.Entity<Basket>()
                .HasMany(bi => bi.Items)
                .WithOne(b => b.Basket)
                .HasForeignKey(bi => bi.BasketId);


            modelBuilder.Entity<BasketItem>()
                .HasOne(bi => bi.Game)
                .WithMany(b=>b.Items)
                .HasForeignKey(bi => bi.GameId);

            modelBuilder.Entity<Game>()
                .HasMany(bi => bi.Items)
                .WithOne(b => b.Game)
                .HasForeignKey(bi => bi.GameId);
        }

    }
}

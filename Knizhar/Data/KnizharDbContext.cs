namespace Knizhar.Data
{
    using Knizhar.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class KnizharDbContext : IdentityDbContext<User>
    {
        public KnizharDbContext(DbContextOptions<KnizharDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; init; }

        public DbSet<Book> Books { get; init; }

        public DbSet<Genre> Genres { get; init; }

        public DbSet<Language> Languages { get; init; }

        public DbSet<Condition> Conditions { get; init; }

        public DbSet<Knizhar> Knizhari { get; init; }

        public DbSet<Town> Towns { get; init; }

        public DbSet<Image> Images { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().Property(b => b.Price).HasPrecision(10, 2);

            modelBuilder
               .Entity<Book>()
               .HasOne(b => b.Author)
               .WithMany(b => b.Books)
               .HasForeignKey(b => b.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<Book>()
               .HasOne(b => b.Language)
               .WithMany(b => b.Books)
               .HasForeignKey(b => b.LanguageId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
              .Entity<Knizhar>()
              .HasOne(t => t.Town)
              .WithMany(k => k.Knizhari)
              .HasForeignKey(t => t.TownId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<Book>()
               .HasOne(b => b.Knizhar)
               .WithMany(k => k.Books)
               .HasForeignKey(k => k.KnizharId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Knizhar>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Knizhar>(k => k.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Book>()
                .HasOne(b => b.Image)
                .WithOne(i => i.Book)
                .HasForeignKey<Book>(b => b.ImageId);
        }
    }
}
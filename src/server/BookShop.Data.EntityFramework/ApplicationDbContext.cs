using BookShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryBook> CategoryBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<CategoryBook>()
                .HasKey(cb => new { cb.CategoryId, cb.BookId });

            builder
                .Entity<CategoryBook>()
                .HasOne(cb => cb.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(cb => cb.CategoryId);

            builder
                .Entity<CategoryBook>()
                .HasOne(cb => cb.Book)
                .WithMany(b => b.Categories)
                .HasForeignKey(cb => cb.BookId);

            builder
                .Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            base.OnModelCreating(builder);
        }
    }
}

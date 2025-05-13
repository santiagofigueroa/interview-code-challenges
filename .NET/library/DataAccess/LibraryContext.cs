using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class LibraryContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookStock> Catalogue { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Fine> Fines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fine>()
                .HasOne(f => f.Borrower)
                .WithMany(b => b.Fines)
                .HasForeignKey(f => f.BorrowerId);

            modelBuilder.Entity<Fine>()
                .HasOne(f => f.Book)
                .WithMany()
                .HasForeignKey(f => f.BookId);
        }
    }
}

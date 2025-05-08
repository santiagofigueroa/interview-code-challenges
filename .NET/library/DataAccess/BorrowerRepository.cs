using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BorrowerRepository : IBorrowerRepository
    {
    
        public BorrowerRepository()
        {
           
        }
        public List<Borrower> GetBorrowers()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Borrowers
                    .ToList();
                return list;
            }
        }

        public Guid AddBorrower(Borrower borrower)
        {
            using (var context = new LibraryContext())
            {
                context.Borrowers.Add(borrower);
                context.SaveChanges();
                return borrower.Id;
            }
        }

        public List<Borrower> GetOnLoan()
        {
            using (var context = new LibraryContext())
            {
                // Query borrowers with active loans and include the titles of books they have on loan
                var borrowersWithLoans = context.Catalogue
                    .Where(bs => bs.OnLoanTo != null) // Filter books that are on loan
                    .Include(bs => bs.Book) // Include book details
                    .Include(bs => bs.OnLoanTo) // Include borrower details
                    .GroupBy(bs => bs.OnLoanTo) // Group by borrower
                    .Select(group => new Borrower
                    {
                        Id = group.Key.Id,
                        Name = group.Key.Name,
                        EmailAddress = group.Key.EmailAddress,
                        BooksOnLoan = group.Select(bs => new Book
                        {
                            Id = bs.Book.Id,
                            Name = bs.Book.Name
                        }).ToList()
                    })
                    .ToList();

                return borrowersWithLoans;
            }
        }
    }
}

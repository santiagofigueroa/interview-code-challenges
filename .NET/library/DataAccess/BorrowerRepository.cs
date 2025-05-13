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

        public bool ReturnBook(Guid bookId)
        {
            using (var context = new LibraryContext())
            {
                var catalogueEntry = context.Catalogue
                    .Include(c => c.Book)
                    .Include(c => c.OnLoanTo)
                    .FirstOrDefault(c => c.Book.Id == bookId && c.OnLoanTo != null);

                if (catalogueEntry == null)
                {
                    return false; // Book not found or not on loan
                }

                //3. Add fine if is past the return date.
                var borrower = catalogueEntry.OnLoanTo;
                var book = catalogueEntry.Book;

                // Simulate loan info 
                var rerturnEndDate = catalogueEntry.LoanEndDate ?? DateTime.UtcNow.AddDays(-7); // default if missing

                // add fine if is past the returnDate
                if (DateTime.UtcNow > rerturnEndDate)
                {
                    var daysLate = (DateTime.UtcNow - rerturnEndDate).Days;
                    var fineAmount = daysLate * 0.55m; // 55p per day

                    var fine = new Fine
                    {
                        BorrowerId = borrower.Id,
                        BookId = book.Id,
                        Amount = fineAmount,
                        IssuedDate = DateTime.UtcNow,
                        Reason = $"Returned {daysLate} day(s) late"
                    };

                    context.Fines.Add(fine);
                }

                // Set this book free to be borrowed again.
                catalogueEntry.OnLoanTo = null;
                context.SaveChanges();
                return true;
            }
        }

        public bool ReserveBook(Guid borrowerId, Guid bookId)
        {
            using (var context = new LibraryContext())
            {
                // Ensure the book exists and is currently on loan
                var catalogueEntry = context.Catalogue
                    .Include(c => c.OnLoanTo)
                    .FirstOrDefault(c => c.Book.Id == bookId);

                if (catalogueEntry == null)
                    return false;

                // Only allow reservation if the book is currently loaned out
                if (catalogueEntry.OnLoanTo == null)
                    return false;

                // Prevent duplicate reservations
                bool alreadyReserved = context.Reservations
                    .Any(r => r.BookId == bookId && r.BorrowerId == borrowerId);

                if (alreadyReserved)
                    return false;

                var reservation = new Reservation
                {
                    BorrowerId = borrowerId,
                    BookId = bookId,
                    ReservedAt = DateTime.UtcNow
                };

                context.Reservations.Add(reservation);
                context.SaveChanges();

                return true;
            }
        }

        public DateTime? GetExpectedAvailability(Guid borrowerId, Guid bookId)
        {
            using (var context = new LibraryContext())
            {
                var reservations = context.Reservations
                    .Where(r => r.BookId == bookId)
                    .OrderBy(r => r.ReservedAt)
                    .ToList();

                var position = reservations.FindIndex(r => r.BorrowerId == borrowerId);
                if (position == -1) return null;

                var loanInfo = context.Catalogue
                    .FirstOrDefault(c => c.Book.Id == bookId);

                if (loanInfo == null || loanInfo.LoanEndDate == null)
                    return null;

                var baseDate = loanInfo.LoanEndDate.Value;
                var estimatedDate = baseDate.AddDays(7 * position); // assume 7 days per borrower
                return estimatedDate;
            }
        }

    }
}

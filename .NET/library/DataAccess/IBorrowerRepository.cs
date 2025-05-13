using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IBorrowerRepository
    {
        public List<Borrower> GetBorrowers();
        public Guid AddBorrower(Borrower borrower);
        public List<Borrower> GetOnLoan();
        bool ReturnBook(Guid bookId);
        bool ReserveBook(Guid borrowerId, Guid bookId);
        DateTime? GetExpectedAvailability(Guid borrowerId, Guid bookId);
    }
}

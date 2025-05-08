using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly ICatalogueRepository _catalogueRepository; 
        private readonly ILogger<BorrowerRepository> _logger;
        
        public BorrowerRepository(ICatalogueRepository catalogueRepository)
        {
            _catalogueRepository = catalogueRepository;
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
                // TODO: Implements Method logic
                var list = _catalogueRepository.GetCatalogue()
                    .Where(x => x.OnLoanTo != null)
                    .Select(x => x.OnLoanTo)
                    .Distinct()
                    .ToList();

                return list;
            }

        }
    }
}

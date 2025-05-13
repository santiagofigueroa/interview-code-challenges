namespace OneBeyondApi.Model
{
    public class Fine
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BorrowerId { get; set; }
        public Borrower Borrower { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }

        public decimal Amount { get; set; }
        public DateTime IssuedDate { get; set; } = DateTime.UtcNow;
        public string Reason { get; set; } = "Late return";

    }
}

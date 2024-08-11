namespace LibrarySystem.Services.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public int BookId { get; set; }        
        public int UserId { get; set; }        
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned => ReturnDate.HasValue;
    }
}

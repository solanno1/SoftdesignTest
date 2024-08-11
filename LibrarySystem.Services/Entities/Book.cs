namespace LibrarySystem.Services.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsRented { get; set; }

    }
}

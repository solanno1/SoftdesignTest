using LibrarySystem.Services.Entities;

namespace LibrarySystem.Services.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks(string search);
        Book GetBookById(int id);
        void UpdateBook(Book book);
        void AddBook(Book book);
    }
}

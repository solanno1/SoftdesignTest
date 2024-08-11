using LibrarySystem.Services.Entities;

namespace LibrarySystem.Services.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks(string search);
        Book GetBookById(int id);
        void RegisterBook(string title, string author);
    }
}

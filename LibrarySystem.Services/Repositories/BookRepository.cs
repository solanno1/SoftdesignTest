using LibrarySystem.Services.Database;
using LibrarySystem.Services.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace LibrarySystem.Services.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Book> GetBooks(string search)
        {
            if(string.IsNullOrEmpty(search)) return _context.Books.ToList();
            return _context.Books.Where(x => x.Title.ToLower().Contains(search.ToLower())).ToList();
        }

        public void UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AddBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Ocorreu um erro, o livro não foi salvo na base de dados.", ex);
            }
        }
    }
}

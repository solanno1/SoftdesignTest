using System.Data.Entity;
using LibrarySystem.Services.Entities;
using MySql.Data.EntityFramework;

namespace LibrarySystem.Services.Database
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() : base("name=LibraryDb")
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }
    }
}

using System.Data.Entity;
using WcfService.Domain.Models;

namespace WcfService.Domain.Context
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext() : base("Data Source=TestingDataBase421.mssql.somee.com;Initial Catalog=TestingDataBase421;User ID=user12;Password=12345678")
        {

        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookStateDbRecord> BookStates { get; set; }


    }
}

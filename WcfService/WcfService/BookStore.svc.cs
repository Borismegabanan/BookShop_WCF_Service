using AutoMapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Configuration;
using WcfService.Contracts.Request;
using WcfService.Contracts.Response;
using WcfService.Domain.Commands;
using WcfService.Domain.Interfaces;
using WcfService.Domain.Models;
using WcfService.Domain.Providers;
using WcfService.Domain.Queries;
using WcfService.Domain.Services;
using WcfService.Mapping;

namespace WcfService
{
    public class BookStore : IBookStore
    {
        private readonly IBookService _bookService;
        private readonly IBookProvider _bookProvider;

        private readonly IMapper _mapper =
            new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())).CreateMapper();

        public BookStore()
        {
            _bookService = new BookService(_mapper);
            _bookProvider = new BookProvider(_mapper);
        }

        /// <summary>
        /// Проверка соединения
        /// </summary>
        /// <returns></returns>
        public string TestConnection()
        {
            return "Ok";
        }

        /// <summary>
        /// Проверка соединения с базой данных
        /// </summary>
        /// <returns></returns>
        public string TestDbConnection()
        {
            var connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["DataBaseConnectionString"].ConnectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        return "Ok";
                    }
                    connection.Close();
                }

            }
            catch
            {
                return "Bad";
            }

            return "Bad";
        }

        public async Task<BookDisplayModel> CreateBook(CreateBookRequest newBook)
        {
            var newBookId = _bookService.CreateBook(_mapper.Map<CreateBookCommand>(newBook));

            var newBookReadModel = await _bookProvider.GetBookReadModelByIdAsync(newBookId);

            return _mapper.Map<BookDisplayModel>(newBookReadModel);
        }

        public Book RemoveBook(int id)
        {
            return _bookService.RemoveBook(new BookQuery() { Id = id });
        }
    }

}

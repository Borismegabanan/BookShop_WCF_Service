using AutoMapper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Data;
using System.Data.SqlClient;
using System.Text;
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
        private IModel Channel;
        private static string _lastMessage;

        private readonly IMapper _mapper =
            new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())).CreateMapper();

        public BookStore()
        {
            _bookService = new BookService(_mapper);
            _bookProvider = new BookProvider(_mapper);

            CreateConsumer();
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

        public string GetLastMessageInQueue()
        {
            return _lastMessage ?? "Queue is empty";
        }

        public Book RemoveBook(int id)
        {
            return _bookService.RemoveBook(new BookQuery() { Id = id });
        }


        private void CreateConsumer()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            Channel = connection.CreateModel();

            Channel.QueueDeclare(queue: "TestWcfPost",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += OnReceive;
            Channel.BasicConsume(queue: "TestWcfPost",
                autoAck: false,
                consumer: consumer);
        }

        private void OnReceive(object sender, BasicDeliverEventArgs e)
        {

            var body = e.Body.ToArray();
            _lastMessage = Encoding.UTF8.GetString(body);

            Channel.BasicAck(e.DeliveryTag, false);
        }

    }

}

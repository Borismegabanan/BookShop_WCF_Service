using System.ServiceModel;
using System.Threading.Tasks;
using WcfService.Contracts.Request;
using WcfService.Contracts.Response;
using WcfService.Domain.Models;

namespace WcfService
{
    [ServiceContract]
    public interface IBookStore
    {
        [OperationContract]
        string TestConnection();

        [OperationContract]
        string TestDbConnection();

        [OperationContract]
        Task<BookDisplayModel> CreateBook(CreateBookRequest newBook);

        [OperationContract]
        string GetLastMessageInQueue();

        [OperationContract]
        Book RemoveBook(int id);
    }
}

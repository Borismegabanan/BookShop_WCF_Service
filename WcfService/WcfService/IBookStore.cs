using System.ServiceModel;
using WcfService.Contracts.Request;
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
        int CreateBook(CreateBookRequest newBook);
        [OperationContract]
        Book RemoveBook(int id);
    }
}

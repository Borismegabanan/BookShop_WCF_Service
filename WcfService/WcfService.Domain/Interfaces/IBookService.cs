using WcfService.Domain.Commands;
using WcfService.Domain.Models;
using WcfService.Domain.Queries;

namespace WcfService.Domain.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// Добавляет запись о новой книге
        /// </summary>
        /// <param name="bookCommand"></param>
        int CreateBook(CreateBookCommand bookCommand);

        /// <summary>
        /// Удаляет книгу
        /// </summary>
        /// <param name="bookQuery"></param>
        Book RemoveBook(BookQuery bookQuery);
    }
}

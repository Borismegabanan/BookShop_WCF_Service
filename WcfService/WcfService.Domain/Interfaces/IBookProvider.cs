using System.Threading.Tasks;
using WcfService.Domain.Common;

namespace WcfService.Domain.Interfaces
{
    public interface IBookProvider
    {
        /// <summary>
        /// получение полный данных о книге по индефикатору.
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Task<ReadModelBook> GetBookReadModelByIdAsync(int bookId);
    }
}

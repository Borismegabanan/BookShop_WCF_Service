using AutoMapper;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WcfService.Domain.Common;
using WcfService.Domain.Context;
using WcfService.Domain.Interfaces;

namespace WcfService.Domain.Providers
{
    public class BookProvider : IBookProvider
    {

        private readonly IMapper _mapper;

        public BookProvider(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ReadModelBook> GetBookReadModelByIdAsync(int bookId)
        {
            using (var context = new ApplicationContext())
            {
                return await context.Books.Where(x => x.Id == bookId)
                    .GroupJoin(context.Authors, x => x.AuthorId, z => z.Id, (book, author) => new { book, author })
                    .Select(x => new ReadModelBook()
                    {
                        Author = x.author.FirstOrDefault().FullName ?? "no author",
                        Id = x.book.Id,
                        Name = x.book.Name,
                        BookStateId = x.book.BookStateId,
                        PublishDate = x.book.PublishDate,
                        InitDate = x.book.InitDate,
                        WhoChanged = x.book.WhoChanged
                    }).FirstOrDefaultAsync();
            }
        }
    }
}

using AutoMapper;
using WcfService.Domain.Commands;
using WcfService.Domain.Context;
using WcfService.Domain.Interfaces;
using WcfService.Domain.Models;
using WcfService.Domain.Queries;

namespace WcfService.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;

        public BookService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public int CreateBook(CreateBookCommand bookCommand)
        {
            var book = _mapper.Map<Book>(bookCommand);
            using (var context = new ApplicationContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
                return book.Id;
            }
        }

        public Book RemoveBook(BookQuery bookQuery)
        {

            using (var context = new ApplicationContext())
            {
                var book = context.Books.Find(bookQuery.Id) ?? new Book();

                context.Books.Remove(book);
                context.SaveChanges();
                return book;
            }
        }
    }
}

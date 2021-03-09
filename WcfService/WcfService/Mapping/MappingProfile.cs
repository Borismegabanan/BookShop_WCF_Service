using AutoMapper;
using System;
using WcfService.Contracts.Request;
using WcfService.Contracts.Response;
using WcfService.Domain.Commands;
using WcfService.Domain.Common;
using WcfService.Domain.Enums;
using WcfService.Domain.Models;

namespace WcfService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReadModelBook, BookDisplayModel>().ForMember(e => e.BookState, opt => opt.MapFrom(c => Enum.GetName(typeof(BookStates), c.BookStateId))).ReverseMap();
            CreateMap<CreateBookCommand, CreateBookRequest>().ReverseMap();
            CreateMap<CreateBookCommand, Book>().BeforeMap((command, book) =>
                {
                    book.InitDate = DateTime.Now;
                    book.WhoChanged = Environment.UserName;
                    book.BookStateId = (int)BookStates.InStock;
                })
                .ReverseMap();
        }
    }
}
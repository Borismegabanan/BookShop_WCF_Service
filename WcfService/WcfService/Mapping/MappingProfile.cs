using AutoMapper;
using System;
using WcfService.Contracts.Request;
using WcfService.Domain.Commands;
using WcfService.Domain.Enums;
using WcfService.Domain.Models;

namespace WcfService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
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
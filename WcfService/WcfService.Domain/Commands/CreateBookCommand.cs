using System;

namespace WcfService.Domain.Commands
{
    public class CreateBookCommand
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
    }
}

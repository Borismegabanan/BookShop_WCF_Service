using System;

namespace WcfService.Contracts.Request
{
    public class CreateBookRequest
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public DateTime PublishDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMicroservices.Mapper.Tests.Models
{
    public class PostContract
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMicroservices.Mapper.Tests.Models
{
    public class UserContract
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public List<PostContract> Posts { get; set; }
    }
}

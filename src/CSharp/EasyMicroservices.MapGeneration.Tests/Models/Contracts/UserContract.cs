﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Tests.Models.Contracts
{
    public class UserContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsMarried { get; set; }
        public int Children { get; set; }
        public string CustomName { get; set; }

        public ProfileContract Profile { get; set; }
        public ICollection<PostContract> Posts { get; set; }
    }
}

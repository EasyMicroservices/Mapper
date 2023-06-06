using System.Collections.Generic;

namespace EasyMicroservices.MapGeneration.Tests.Models.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public int ChildrenCount { get; set; }
        public ProfileEntity Profile { get; set; }
        public ICollection<PostEntity> Posts { get; set; }
    }
}

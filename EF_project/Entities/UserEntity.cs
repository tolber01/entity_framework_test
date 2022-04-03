using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF_project.Entities
{
    public class UserEntity
    {
        [Key]
        public string NickName { get; set; }
        public string Email { get; set; }
        public List<ChatEntity> UserChats { get; set; } = new List<ChatEntity>();
    }
}

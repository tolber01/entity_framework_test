using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF_project.Entities
{
    public class ChatEntity
    {
        [Key]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public List<MessageEntity> Messages { get; set; } = new List<MessageEntity>();
        public List<UserEntity> Participants { get; set; } = new List<UserEntity>();
    }
}
